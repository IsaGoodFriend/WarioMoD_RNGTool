using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarioMoD.RNG {
	[Flags]
	public enum RedChestType {
		None,
		BallPath = 0x01, // 2
		Roaches = 0x02, // 4
		DrawAPath = 0x04, // 6
		Match5 = 0x10, // 8
		SlidePuzzle = 0x20, // 10
		ConnectDots = 0x40, // 12
		Trace = 0x80,
		All = 0x8F,
	}
	public enum TraceType {
		WSymbol,
		HatThing,
		NoseStache,
		Diamond,
		Garlics,
		MoneyBag,
		Cannoli,
		Goodstyle,
		TV,
		Car,
		Hat,
		Shoe,
	}

	public class RedPurpleChest {

		public int Cost;

		public RedChestType currentType;

		public string ExData;

		public RedPurpleChest(RouteFinder router, bool inLevel = true) : this(GetChestType(router), router, inLevel) {

		}
		public RedPurpleChest(RedChestType type, RouteFinder router, bool inLevel = true) {

			var random = router.random;

			currentType = type;

			switch (currentType) {
				case RedChestType.Trace:
					int offset;

					do {
						offset = random.GetRange(12);
					}
					while (offset == 0);

					switch ((TraceType)offset) {
						case TraceType.TV:
							Cost += 25;
							break;
						case TraceType.HatThing:
							Cost += 5;
							break;
						case TraceType.NoseStache:
							Cost += 10;
							break;
						case TraceType.Diamond:
							Cost += 0;
							break;
						case TraceType.MoneyBag:
							Cost += 5;
							break;
						case TraceType.Cannoli:
							Cost += 20;
							break;
						case TraceType.Goodstyle:
							Cost += 20;
							break;
						case TraceType.Car:
							Cost += 20;
							break;
						case TraceType.Garlics:
							Cost += 30;
							break;
						case TraceType.Hat:
							Cost += 5;
							break;
						case TraceType.Shoe:
							Cost += 5;
							break;
					}

					if (RouteFinder.HumanRunning) {
						Cost *= 3;
					}

					ExData = ((TraceType)offset).ToString();

					random.GetInteger();

					Cost += RouteFinder.HumanRunning ? 130 : 105;

					break;
				case RedChestType.BallPath:
					random.GetInteger();

					Cost = RouteFinder.HumanRunning ? 65 : 0;
					break;
				case RedChestType.DrawAPath:

					Cost = RouteFinder.HumanRunning ? 60 : 130;

					int layout;

					if (inLevel) {

						do {
							layout = random.GetRange(5);
						}
						while (layout >= 5);
					}

					int dynamiteCount = 0, staffCount = 0;

					if (inLevel) {

						switch (router.CurrentLevel) {
							case 3:
							case 4:
								dynamiteCount = 2;
								staffCount = 6;
								break;
							case 5:
							case 6:
								dynamiteCount = 3;
								staffCount = 8;
								break;
							case 7:
								dynamiteCount = 4;
								staffCount = 8;
								break;
							case 8:
								dynamiteCount = 4;
								staffCount = 9;
								break;
							case 9:
								dynamiteCount = 4;
								staffCount = 9;
								break;
							case 10:
								dynamiteCount = 4;
								staffCount = 10;
								break;
						}
					}
					else {
						switch (router.CurrentLevel) {
							case 0:
							case 1:
								dynamiteCount = 1;
								staffCount = 6;
								break;
							case 5:
							case 6:
								dynamiteCount = 3;
								staffCount = 8;
								break;
							case 7:
								dynamiteCount = 4;
								staffCount = 8;
								break;
							case 8:
								dynamiteCount = 4;
								staffCount = 9;
								break;
							case 9:
								dynamiteCount = 4;
								staffCount = 9;
								break;
							case 10:
								dynamiteCount = 4;
								staffCount = 10;
								break;
						}
					}


					List<int> dynamite = new List<int>();

					while (dynamite.Count < dynamiteCount) {
						int val = random.GetRange(staffCount);
						if (!dynamite.Contains(val)) {
							dynamite.Add(val);
						}
					}

					random.GetInteger();

					foreach (var d in dynamite) {
						ExData += $"{d}; ";
					}

					//while (true) {
					//	if (rng.GetRange(6) >= 2)
					//		break;
					//}

					//rng.RunRNGBase();
					break;
				case RedChestType.Match5:
					// Who cares, it's ignored
					Cost = 100000;

					break;
				case RedChestType.SlidePuzzle:
					// which picture?
					string pic = new string[]{ "hat", "shoe", "garlic" }[random.GetRange(3)];

					int count = 0;
					switch (router.CurrentLevel) {
						case 1:
						case 2:
						case 3:
						case 4:
							count = 5;
							break;
						case 5:
						case 6:
							count = 6;
							break;
						case 7:
						case 8:
							count = 7;
							break;
						case 9:
						case 10:
							count = 8;
							break;
					}
					int x = 2, y = 2, lastdir = -1;

					Cost = Math.Max(count, 0) * (RouteFinder.HumanRunning ? 20 : 10);
					//Cost -= 15;

					const int up = 2, down = 3, left = 0, right = 1;

					while (count > 0) {

						List<int> directions = new List<int>();

						if (x > 0) {
							directions.Add(left);
						}
						if (x < 2) {
							directions.Add(right);
						}
						if (y > 0) {
							directions.Add(up);
						}
						if (y < 2) {
							directions.Add(down);
						}

						long value = (uint)random.GetInteger();

						int dir = (int)(value % directions.Count);
						dir = directions[dir];


						if (dir == (lastdir ^ 1)) {
							continue;
						}


						if (dir == left && x > 0) {
							x--;
							count--;
							lastdir = left;
							continue;
						}
						if (dir == right && x < 2) {
							x++;
							count--;
							lastdir = right;
							continue;
						}
						if (dir == up && y > 0) {
							y--;
							count--;
							lastdir = up;
							continue;
						}
						if (dir == down && y < 2) {
							y++;
							count--;
							lastdir = down;
							continue;
						}

					}

					break;
				case RedChestType.ConnectDots:
					random.GetInteger();
					random.GetInteger();
					random.GetInteger();

					Cost = RouteFinder.HumanRunning ? 30 : 0;
					break;
				case RedChestType.Roaches:
					random.GetInteger();
					random.GetInteger();	

					Cost = RouteFinder.HumanRunning ? 450 : 350;
					break;
				default:
					//rng.RunRNGBase();
					break;
			}
		}

		static RedChestType GetChestType(RouteFinder router) {
			RedChestType t;
			var rng = router.random;

			if (rng.GetRange(101) <= router.TraceDrought * 10)
				t = RedChestType.Trace;
			else {
				t = ChestTypeRNG(rng);
				while (t == router.Previous || (t & router.Pool) != RedChestType.None) {
					t = ChestTypeRNG(rng);
				}
			}

			if (router.Pool == RedChestType.All) {
				router.Pool = RedChestType.None;
				router.TraceDrought = 0;
			}

			router.Previous = t;
			router.Pool |= t;

			if (t == RedChestType.Trace) {
				router.TraceDrought = 0;
			}
			else {
				router.TraceDrought++;
			}


			return t;
		}
		static RedChestType ChestTypeRNG(WarioRandom random) {

			var input = random.GetRange(12);

			int output = input >> 1;

			switch (output) {
				case 0:
					return RedChestType.BallPath;
				case 1:
					return RedChestType.Roaches;
				case 2:
					return RedChestType.DrawAPath;
				case 3:
					return RedChestType.Match5;
				case 4:
					return RedChestType.SlidePuzzle;
				case 5:
					return RedChestType.ConnectDots;
			}

			return (RedChestType)output;
		}
	}
}
