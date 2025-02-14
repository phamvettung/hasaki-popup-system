namespace Intech_software.DTO
{
    internal class InboundState
    {
        public int Zone1Count { get; set; } = 0;
        public int Zone2Count { get; set; } = 0;
        public int Zone3Count { get; set; } = 0;
        public int Zone4Count { get; set; } = 0;
        public int Zone5Count { get; set; } = 0;
        public int Zone6Count { get; set; } = 0;
        public int Zone7Count { get; set; } = 0;
        public int Zone8Count { get; set; } = 0;
        public int Zone9Count { get; set; } = 0;
        public int Zone0Count { get; set; } = 0;
        public int Shop22Count { get; set; } = 0;
        public int Zone99Count { get; set; } = 0;
        public int Zone1ACount { get; set; } = 0;
        public int Zone2ACount { get; set; } = 0;
        public int Zone3ACount { get; set; } = 0;
        public int Zone4ACount { get; set; } = 0;
        public int Zone5ACount { get; set; } = 0;
        public int Zone6ACount { get; set; } = 0;
        public int Zone7ACount { get; set; } = 0;
        public int Zone8ACount { get; set; } = 0;
        public int Zone9ACount { get; set; } = 0;
        public int Zone6BCount { get; set; } = 0;

        public void IncrZoneCount(string zone)
        {
            switch (zone)
            {
                case "0":
                    Zone0Count += 1;
                    break;
                case "1":
                    Zone1Count += 1;
                    break;
                case "2":
                    Zone2Count += 1;
                    break;
                case "3":
                    Zone3Count += 1;
                    break;
                case "4":
                    Zone4Count += 1;
                    break;
                case "5":
                    Zone5Count += 1;
                    break;
                case "6":
                    Zone6Count += 1;
                    break;
                case "7":
                    Zone7Count += 1;
                    break;
                case "8":
                    Zone8Count += 1;
                    break;
                case "9":
                    Zone9Count += 1;
                    break;
                case "SHOPEE":
                    Shop22Count += 1;
                    break;
                case "99":
                    Zone99Count += 1;
                    break;
                case "1A":
                    Zone1ACount += 1;
                    break;
                case "2A":
                    Zone2ACount += 1;
                    break;
                case "3A":
                    Zone3ACount += 1;
                    break;
                case "4A":
                    Zone4ACount += 1;
                    break;
                case "5A":
                    Zone5ACount += 1;
                    break;
                case "6A":
                    Zone5ACount += 1;
                    break;
                case "7A":
                    Zone7ACount += 1;
                    break;
                case "8A":
                    Zone8ACount += 1;
                    break;
                case "9A":
                    Zone9ACount += 1;
                    break;
                case "6B":
                    Zone6BCount += 1;
                    break;
                default:
                    Zone0Count += 1;
                    break;
            }
        }

    }
}
