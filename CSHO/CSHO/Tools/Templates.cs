namespace CSHO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HoArchive;

public partial class Handler
{
    private static int Parse(string num)
    {
        return num.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            ? Convert.ToInt32(num.Substring(2), 16)
            : int.Parse(num);
    }

    private class Link
    {
        public uint offset;
        public string targetAsset;

        public Link(string argument)
        {
            offset = (uint)Parse(argument.Split(" ")[0]);
            targetAsset = String.Join(" ", argument.Split(" ").Skip(1));
        }
    }

    private class TemplateEntry
    {
        public ulong id; // Not user assignable for now, 
        public List<byte> data;

        public string ASSET = "";
        public string PATH = "";
        public string NAME = "";
        public wmlTypeID TYPE = wmlTypeID.Accomplishment;

        public uint BLOBALIGN = 4;
        public ushort BLOBFLAGS = 1;
        public ushort SUBTYPE = 0;

        public string PARCEL = "P";
        public uint TABLE = 0;

        public List<Link> LINKS = new();


        public TemplateEntry() { }


        public TemplateEntry Copy()
        {
            TemplateEntry copy = new TemplateEntry();

            copy.ASSET = ASSET;
            copy.PATH = PATH;
            copy.NAME = NAME;
            copy.TYPE = TYPE;
            copy.BLOBALIGN = BLOBALIGN;
            copy.BLOBFLAGS = BLOBFLAGS;
            copy.SUBTYPE = SUBTYPE;
            copy.PARCEL = PARCEL;
            copy.TABLE = TABLE;
            copy.LINKS = new List<Link>(LINKS);

            return copy;
        }

        public void SetVar(string command, string argument)
        {
            switch (command)
            {
                case "ASSET":
                    ASSET = argument;
                    break;

                case "PATH":
                    PATH = argument;
                    break;

                case "NAME":
                    NAME = argument;
                    break;

                case "TYPE":
                    TYPE = (wmlTypeID)Enum.Parse(typeof(wmlTypeID), argument);
                    break;

                case "BLOBALIGN":
                    BLOBALIGN = (uint)Parse(argument);
                    break;

                case "BLOBFLAGS":
                    BLOBFLAGS = (ushort)Parse(argument);
                    break;

                case "SUBTYPE":
                    SUBTYPE = (ushort)Parse(argument);
                    break;

                case "PARCEL":
                    PARCEL = argument;
                    break;

                case "TABLE":
                    TABLE = (uint)Parse(argument);
                    break;

                case "LINK":
                    LINKS.Add(new Link(argument));
                    break;

                case "CLEARLINKS":
                    LINKS = new();
                    break;
            }
        }
    }

    private static TemplateEntry FindEntry(List<TemplateEntry> entries, string ASSET)
    {
        foreach (TemplateEntry entry in entries) {
            if (entry.ASSET == ASSET) return entry;
        }

        return null;
    }
    

    private static string ResolvePath(string referencePath, string originalFilePath)
    {
        return Path.IsPathRooted(referencePath)
            ? Path.GetFullPath(referencePath) // absolute already
            : Path.GetFullPath(Path.Combine(Path.GetDirectoryName(originalFilePath)!, referencePath));
    }

    public string ImportTemplate(string path, out List<TOCEntry> output)
    {
        output = new();

        if (!File.Exists(path))
        {
            return "ERR_FILE_NOT_FOUND";
        }

        // CODEGEN from ChatGPT [Generated on 8.9.2025]
        var cleanedLines = File.ReadAllLines(path)
            .Select(line =>
            {
                // Remove comments
                int commentIndex = line.IndexOf("//");
                if (commentIndex >= 0)
                    line = line.Substring(0, commentIndex);

                // Replace tabs with spaces
                line = line.Replace("\t", " ");

                // Collapse multiple spaces into one
                line = Regex.Replace(line, @"\s+", " ");

                // Trim whitespace
                return line.Trim();
            })
            .Where(line => !string.IsNullOrWhiteSpace(line)) // Remove empty lines
            .ToList();

        // CODEGEN end


        int currentVersion = -1;

        TemplateEntry currentTemplateEntry = new TemplateEntry();
        List<TemplateEntry> templateEntries = new();

        // Collect all template entries
        try
        {
            foreach (string line in cleanedLines)
            {
                string command = line.Split(" ")[0];
                string argument = String.Join(" ", line.Split(" ").Skip(1));

                if (command != "VERSION" && (currentVersion == -1)) return "ERR_NO_VERSION_SPECIFIED";

                switch (command)
                {
                    case "VERSION":
                        currentVersion = int.Parse(argument);
                        break;

                    case "IMPORT": // currently configured templateentry should be imported, copy it into list
                        templateEntries.Add(currentTemplateEntry.Copy());
                        break;

                    default:
                        currentTemplateEntry.SetVar(command, argument);
                        break;

                }
            }
        }
        catch (Exception e)
        {
            return "ERR_" + e.ToString();
        }

        // Assign asset IDs to all entries
        foreach (TemplateEntry entry in templateEntries)
        {
            List<ulong> ids = new();
            entry.id = GenerateAssetID(ids);
            ids.Add(entry.id);
        }

        // Read Data
        foreach (TemplateEntry entry in templateEntries)
        {
            string datapath = ResolvePath(entry.PATH, path); 

            if (!File.Exists(datapath)) return "ERR_FILE_NOT_FOUND: " + datapath;

            entry.data = File.ReadAllBytes(datapath).ToList();
        }

        // Update AssetID links
        foreach (TemplateEntry entry in templateEntries)
        {
            foreach (Link link in entry.LINKS)
            {
                TemplateEntry linkend = FindEntry(templateEntries, link.targetAsset);

                if (linkend == null) return "ERR_ASSET_LINK_NOT_FOUND: " + entry.ASSET + ", " + link.targetAsset;

                if ((link.offset < 0)
                || ((link.offset + 8) > entry.data.Count)) return "ERR_OFFSET_OUT_OF_RANGE: " + entry.ASSET + ", " + link.offset;

                List<byte> replacementdata = BitConverter.GetBytes(linkend.id).ToList(); if (BitConverter.IsLittleEndian != endian) replacementdata.Reverse();

                entry.data.RemoveRange((int)link.offset, 8);
                entry.data.InsertRange((int)link.offset, replacementdata);


            }
        }

        // Now import the template entries!
        try
        {
            foreach (TemplateEntry entry in templateEntries)
            {
                TOCEntry tocentry = new TOCEntry(entry.id, entry.TYPE, entry.BLOBALIGN, entry.SUBTYPE, entry.BLOBFLAGS, entry.data);
                tocentry.CreateEntity(new MemoryStreamEndian(tocentry.data.ToArray(), endian), Archive.Header.target, Archive.Header.platform);
                tocentry.Update();

                string error = NewNameTableEntry(entry.id, entry.NAME);
                if (error != "") return error;

                error = NewAsset(tocentry, targetparcel: entry.PARCEL, targettoc: (int)entry.TABLE);
                if (error != "") return error;

                output.Add(tocentry);
            }
        }
        catch (Exception e)
        {
            return "ERR_" + e.ToString();
        }


        return "";
    }
}