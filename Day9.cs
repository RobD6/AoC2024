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

        long checksum = CalculateChecksum(usedList);

        Console.WriteLine(checksum);
    }

    public void Part2()
    {
        var data = Utils.LoadData(9).Trim();
        var usedList = new List<UsedBlock>();
        var freeList = new List<FreeBlock>();

        ParseData(data, usedList, freeList);

        Defrag2(usedList, freeList);

        //Calculate checksum
        long checksum = 0;
        checksum = CalculateChecksum(usedList);

        Console.WriteLine(checksum);
    }

    private void Defrag(List<UsedBlock> usedList, List<FreeBlock> freeList)
    {
        while (freeList[0].Start < usedList[^1].Start)
        {
            var usedBlock = usedList[^1];
            var freeBlock = freeList[0];

            //The order of the used list isn't really important, so just move used blocks to the front
            //of the list when they're dealt with
            if (usedBlock.Length == freeBlock.Length)
            {
                usedBlock.Start = freeBlock.Start;
                freeList.Remove(freeBlock);
                usedList.Remove(usedBlock);
                usedList.Insert(0, usedBlock);
            }
            else if (freeBlock.Length > usedBlock.Length)
            {
                usedBlock.Start = freeBlock.Start;
                freeBlock.Start += usedBlock.Length;
                freeBlock.Length -= usedBlock.Length;
                usedList.Remove(usedBlock);
                usedList.Insert(0, usedBlock);
            }
            else
            {
                usedList.Insert(0, new UsedBlock(){Start = freeBlock.Start, FileId = usedBlock.FileId, Length = freeBlock.Length});
                usedBlock.Length -= freeBlock.Length;
                freeList.Remove(freeBlock);
            }
        }
    }

    private void Defrag2(List<UsedBlock> usedList, List<FreeBlock> freeList)
    {
        List <UsedBlock> defragOrder = [];
        defragOrder.AddRange(usedList);
        defragOrder.Reverse();

        foreach (var usedBlock in defragOrder)
        {
            var freeBlock = freeList.FirstOrDefault(f => f.Length >= usedBlock.Length);
            if (freeBlock != null && freeBlock.Start < usedBlock.Start)
            {
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
            }
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

    private static long CalculateChecksum(List<UsedBlock> usedList)
    {
        long checksum = 0;
        foreach (var used in usedList)
        {
            for (int i = 0; i < used.Length; i++)
            {
                checksum += (used.Start + i) * used.FileId;
            }
        }

        return checksum;
    }
}