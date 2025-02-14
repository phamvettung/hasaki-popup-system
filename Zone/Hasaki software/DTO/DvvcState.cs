using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech_software.DTO
{
    internal class DvvcState
    {
        public int CountTotal { get; set; } = 0;
        public int CountError { get; set; } = 0;
        public int CountSuccess { get; set; } = 0;
        public int CountGate1 { get; set; } = 0;
        public int CountGate2 { get; set; } = 0;
        public int CountGate3 { get; set; } = 0;
        public int CountGate4 { get; set; } = 0;
        public int CountGate5 { get; set; } = 0;
        public int CountGate6 { get; set; } = 0;
        public int CountGate7 { get; set; } = 0;
        public int CountGate8 { get; set; } = 0;
        public int CountGate9 { get; set; } = 0;
        public int CountGate10 { get; set; } = 0;

        public void ResetAll()
        {
            CountTotal = 0;
            CountError = 0;
            CountSuccess = 0;
            CountGate1 = 0;
            CountGate2 = 0;
            CountGate3 = 0;
            CountGate4 = 0;
            CountGate5 = 0;
            CountGate6 = 0;
            CountGate7 = 0;
            CountGate8 = 0;
            CountGate9 = 0;
            CountGate10 = 0;
        }

        public int GetGateCount(int gate)
        {
            switch (gate)
            {
                case 1:
                    return CountGate1;
                case 2:
                    return CountGate2;
                case 3:
                    return CountGate3;
                case 4:
                    return CountGate4;
                case 5:
                    return CountGate5;
                case 6:
                    return CountGate6;
                case 7:
                    return CountGate7;
                case 8:
                    return CountGate8;
                case 9:
                    return CountGate9;
                case 10:
                    return CountGate10;
                default:
                    return 0;
            }
        }
        public void IncrGateCount(int gate, int quantity = 1)
        {
            switch (gate)
            {
                case 1:
                    CountGate1 += quantity;
                    break;
                case 2:
                    CountGate2 += quantity;
                    break;
                case 3:
                    CountGate3  += quantity;
                    break;
                case 4:
                    CountGate4  += quantity;
                    break;
                case 5:
                    CountGate5  += quantity;
                    break;
                case 6:
                    CountGate6  += quantity;
                    break;
                case 7:
                    CountGate7  += quantity;
                    break;
                case 8:
                    CountGate8  += quantity;
                    break;
                case 9:
                    CountGate9  += quantity;
                    break;
                case 10:
                    CountGate10  += quantity;
                    break;
            }

            if (gate >= 1 && gate <= 10)
            {
                CountSuccess += quantity;
            }
            else
            {
                CountError += quantity;
            }
            CountTotal += quantity;
        }

        public void SetGateValue(int gate, int value)
        {
            switch (gate)
            {
                case 1:
                    CountGate1 = value;
                    break;
                case 2:
                    CountGate2 = value;
                    break;
                case 3:
                    CountGate3 = value;
                    break;
                case 4:
                    CountGate4 = value;
                    break;
                case 5:
                    CountGate5 = value;
                    break;
                case 6:
                    CountGate6 = value;
                    break;
                case 7:
                    CountGate7 = value;
                    break;
                case 8:
                    CountGate8 = value;
                    break;
                case 9:
                    CountGate9 = value;
                    break;
                case 10:
                    CountGate10 = value;
                    break;
            }
        }
    }
}
