using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRDuino.Models.State
{
    public enum Codes
    {
        CODE_FILLER = 620,
        CODE_HIGH = 1600,
        CODE_LOW = 540,
        CODE_FIRST = 9000,
        CODE_SECOND = 4000,
        CODE_THRESHOLD = 700,
        CODE_PAUSE = 19000
    }

    public class GreeState
    {
        private readonly int _bufferSize;

        public ushort[] Codes { get; }

        public ushort Temperature { get; private set; }

        public ushort Mode { get; private set; }

        public ushort Fan { get; private set; }

        public ushort State { get; private set; }

        public GreeState()
        {
            _bufferSize = 139;
            Codes = new ushort[_bufferSize + 1];

            SetUpStaticCodes();
        }

        public void SetTemperature(ushort temperature)
        {
            Temperature = temperature;

            if ((Temperature & 8) != 0)
            {
                Codes[25] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[25] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((Temperature & 4) != 0)
            {
                Codes[23] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[23] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((Temperature & 2) != 0)
            {
                Codes[21] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[21] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((Temperature & 1) != 0)
            {
                Codes[19] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[19] = (ushort)Models.State.Codes.CODE_LOW;
            }

            SetCheckSum();
        }

        public void SetMode(ushort mode)
        {
            Mode = mode;

            if ((Mode & 4) != 0)
            {
                Codes[7] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[7] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((Mode & 2) != 0)
            {
                Codes[5] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[5] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((Mode & 1) != 0)
            {
                Codes[3] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[3] = (ushort)Models.State.Codes.CODE_LOW;
            }

            SetCheckSum();
        }

        public void SetFan(ushort fan)
        {
            Fan = fan;

            if ((Fan & 2) != 0)
            {
                Codes[13] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[13] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((Fan & 1) != 0)
            {
                Codes[11] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[11] = (ushort)Models.State.Codes.CODE_LOW;
            }

            SetCheckSum();
        }

        public void SetState(ushort state)
        {
            State = state;

            if ((State & 1) != 0)
            {
                Codes[9] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[9] = (ushort)Models.State.Codes.CODE_LOW;
            }

            SetCheckSum();
        }

        private void SetCheckSum()
        {
            ushort block = 0;
            for (ushort i = 3; i <= 17; i = (ushort)(i + 2))
            {
                if (Codes[i] > (ushort)Models.State.Codes.CODE_THRESHOLD)
                {
                    block++;
                }

                if (i <= 15)
                {
                    block <<= 1;
                }
            }

            ushort b1 = block;
            b1 = ReverseBit(b1);
            b1 = (ushort)(b1 & 0x0F);
            block = 0;

            for (ushort i = 19; i <= 33; i = (ushort)(i + 2))
            {
                if (Codes[i] > (ushort)Models.State.Codes.CODE_THRESHOLD)
                {
                    block++;
                }

                if (i <= 31)
                {
                    block <<= 1;
                }
            }

            ushort b2 = block;
            b2 = ReverseBit(b2);
            b2 = (ushort)(b2 & 0x0F);

            ushort bit_sum = (ushort)(b1 + b2 + 2 + 10);
            ushort crc = (ushort)(bit_sum & 0xF);

            if ((crc & 1) != 0)
            {
                Codes[131] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[131] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((crc & 2) != 0)
            {
                Codes[133] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[133] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((crc & 4) != 0)
            {
                Codes[135] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[135] = (ushort)Models.State.Codes.CODE_LOW;
            }

            if ((crc & 8) != 0)
            {
                Codes[137] = (ushort)Models.State.Codes.CODE_HIGH;
            }
            else
            {
                Codes[137] = (ushort)Models.State.Codes.CODE_LOW;
            }
        }

        private ushort ReverseBit(ushort value)
        {
            value = (ushort) (((value >> 1) & 0x55) | ((value << 1) & 0xaa));
            value = (ushort) (((value >> 2) & 0x33) | ((value << 2) & 0xcc));
            value = (ushort) (((value >> 4) & 0x0f) | ((value << 4) & 0xf0));

            return value;
        }

        private void SetUpStaticCodes()
        {
            for (byte i = 0; i < _bufferSize; i++)
            {
                if (i % 2 == 0)
                {
                    Codes[i] = (ushort)Models.State.Codes.CODE_FILLER;
                }
                else
                {
                    Codes[i] = (ushort)Models.State.Codes.CODE_LOW;
                }
            }

            Codes[0] = (ushort)Models.State.Codes.CODE_FIRST;
            Codes[1] = (ushort)Models.State.Codes.CODE_SECOND;
            Codes[45] = (ushort)Models.State.Codes.CODE_HIGH;
            Codes[59] = (ushort)Models.State.Codes.CODE_HIGH;
            Codes[63] = (ushort)Models.State.Codes.CODE_HIGH;
            Codes[69] = (ushort)Models.State.Codes.CODE_HIGH;
            Codes[101] = (ushort)Models.State.Codes.CODE_HIGH;
            Codes[73] = (ushort)Models.State.Codes.CODE_PAUSE;
            Codes[139] = (ushort)Models.State.Codes.CODE_FILLER;
        }
    }
}
