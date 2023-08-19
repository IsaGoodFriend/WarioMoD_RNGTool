using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WarioMoD.RNG {
	public class WarioRandom {
		static long GetLong(string value, bool reverse = true) {
			if (!reverse)
				return Convert.ToInt64(value, 16);

			long retval = 0;
			for (int i = 0; i < 8; i++) {
				retval = (retval << 8) | Convert.ToInt64(value.Substring(value.Length - 2, 2), 16);
				value = value.Substring(0, value.Length - 2);
			}

			return retval;
		}


		// 378A3F5D10127596
		public static int? Distance(long start, long end) {

			if (start == end)
				return 0;

			var rng = new WarioRandom(start);

			for (int i = 1; i <= 1000; i++) {
				rng.GetInteger();

				if (rng.Seed == end)
					return i;
			}
			rng.Seed = end;

			for (int i = 1; i <= 1000; i++) {
				rng.GetInteger();

				if (rng.Seed == start)
					return -i;
			}

			return null;
		}
		public static int? Distance(string start, string end, bool reverseStart = false, bool reverseEnd = false) {

			return Distance(GetLong(start, reverseStart), GetLong(end, reverseEnd));
		}

		public long Seed;
		const long 
			rngPlanter1 = 0x5D588B656C078965,
			rngPlanter2 = 0x0000000000269EC3;

		public WarioRandom(long seed) {
			Seed = seed;
		}

		public WarioRandom(string seed) {
			Seed = GetLong(seed, true);
		}

		public int GetInteger() {

			long temp1 = Seed * rngPlanter1;
			temp1 += rngPlanter2;

			Seed = temp1;

			return (int)(temp1 >> 32);
		}
		public int GetRange(int max) {
			long val = (uint)GetInteger();

			return (int)((val * max) >> 32);
		}

		public override string ToString() {
			return Seed.ToString("x16");
		}
	}
}
