using System.Text.RegularExpressions;

namespace AoC2024;

public class Day9
{
    class UsedBlock
    {
        public int Start;
        public int FileId;
        public int Length;
    }

    class FreeBlock
    {
        public int Start;
        public int Length;
    }

    public void Part1()
    {
        var data = Utils.LoadData(9).Trim();
        var usedList = new List<UsedBlock>();
        var freeList = new List<FreeBlock>();

        ParseData(data, usedList, freeList);

        Defrag(usedList, freeList);

        //Calculate checksum
        long checksum = 0;
        foreach (var used in usedList)
        {
            for (int i = 0; i < used.Length; i++)
            {
                checksum += (used.Start + i) * used.FileId;
            }
        }

        Console.WriteLine(checksum);
    }

    private void Defrag(List<UsedBlock> usedList, List<FreeBlock> freeList)
    {
        while (freeList[0].Start < usedList[^1].Start)
        {
            var usedBlock = usedList[^1];
            var freeBlock = freeList[0];

            if (usedBlock.Length == freeBlock.Length)
            {
                usedBlock.Start = freeBlock.Start;
                freeList.Remove(freeBlock);
            }
            else if (freeBlock.Length > usedBlock.Length)
            {
                usedBlock.Start = freeBlock.Start;
                freeBlock.Start += usedBlock.Length;
                freeBlock.Length -= usedBlock.Length;
            }
            else
            {
                usedList.Add(new UsedBlock(){Start = freeBlock.Start, FileId = usedBlock.FileId, Length = freeBlock.Length});
                freeBlock.Start = usedBlock.Start;
                usedBlock.Start += freeBlock.Length;
                usedBlock.Length -= freeBlock.Length;
                freeList.Sort((a, b) => (a.Start.CompareTo(b.Start)));
            }

            usedList.Sort((a, b) => (a.Start.CompareTo(b.Start)));
        }
    }

    private static void ParseData(string data, List<UsedBlock> usedList, List<FreeBlock> freeList)
    {
        int address = 0;
        bool isData = true;
        int nextFileId = 0;

        foreach (char cr in data)
        {
            int blockLength = int.Parse(cr.ToString());
            if (isData)
            {
                var block = new UsedBlock() { Start = address, FileId = nextFileId, Length = blockLength };
                usedList.Add(block);
                nextFileId++;
            }
            else
            {
                if (blockLength != 0)
                {
                    var block = new FreeBlock() { Start = address, Length = blockLength };
                    freeList.Add(block);
                }
            }

            address += blockLength;
            isData = !isData;
        }
    }


    public void Part2()
    {
        var data = Utils.LoadData(9);
    }

}