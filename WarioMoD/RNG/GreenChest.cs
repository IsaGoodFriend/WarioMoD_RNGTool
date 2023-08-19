using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarioMoD.RNG {

	public enum GreenChestType {
		Cosmic,
		Arty,
		Dragon,
		Genius,
		Devil,
		Sparky,
		Captain,
	}
	public class GreenChest {
		public List<int> FilledPieces = new List<int>();

		public int Cost = 0;

		public GreenChestType Type;
		public bool Upgrade;

		public GreenChest(GreenChestType type, bool upgrade, WarioRandom rng) {

			Type = type;
			Upgrade = upgrade;

			int totalPieces, filledPieces;

			switch (type) {
				case GreenChestType.Cosmic:
					totalPieces = upgrade ? 6 : 5;
					filledPieces = upgrade ? 3 : 3;

					break;
				case GreenChestType.Arty:
					totalPieces = upgrade ? 6 : 4;
					filledPieces = upgrade ? 4 : 2;

					break;
				case GreenChestType.Genius:
					totalPieces = upgrade ? 6 : 5;
					filledPieces = upgrade ? 3 : 3;

					break;
				case GreenChestType.Captain:
					totalPieces = upgrade ? 8 : 6;
					filledPieces = upgrade ? 3 : 3;

					break;
				case GreenChestType.Sparky:
					totalPieces = 4;
					filledPieces = 2;

					break;
				case GreenChestType.Dragon:
					totalPieces = upgrade ? 7 : 4;
					filledPieces = upgrade ? 3 : 2;

					break;
				case GreenChestType.Devil:
					totalPieces = 8;
					filledPieces = 3;

					break;
				default:
					throw new Exception();
			}


			while (FilledPieces.Count < filledPieces) {

				int end = rng.GetRange(totalPieces);

				if (!FilledPieces.Contains(end))
					FilledPieces.Add(end);
			}

			if (RouteFinder.HumanRunning) {
				List<int> costs = new List<int>();

				switch (type) {
					case GreenChestType.Cosmic:
						if (upgrade) {

							///if \((FilledPieces.Contains\([0-9]\)\))
							if (!FilledPieces.Contains(0)) {
								costs.Add(120);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(15);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(30);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(25);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(40);
							}
							if (!FilledPieces.Contains(5)) {
								costs.Add(25);
							}
							// base, rod, orb, wing, handle, base
						}
						else {
							if (!FilledPieces.Contains(0)) { // helmet
								costs.Add(1000);
							}
							if (!FilledPieces.Contains(1)) { // shine
								costs.Add(20);
							}
							if (!FilledPieces.Contains(2)) { // base
								costs.Add(40);
							}
							if (!FilledPieces.Contains(3)) { // left handle
								costs.Add(35);
							}
							if (!FilledPieces.Contains(4)) { // right handle
								costs.Add(35);
							}
						}

						break;
					case GreenChestType.Arty:
						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								costs.Add(35);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(20);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(250);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(10);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(10);
							}
							if (!FilledPieces.Contains(5)) {
								costs.Add(10);
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								costs.Add(120);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(30);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(25);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(20);
							}
						}
						// base, stripe, bean, rim
						// bristles, handle, base, purple, pink, blue

						break;
					case GreenChestType.Genius:
						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								costs.Add(15);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(35);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(90);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(40);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(25);
							}
							if (!FilledPieces.Contains(5)) {
								costs.Add(10);
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								costs.Add(120);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(35);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(10);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(15);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(5);
							}
						}
						// frame, glass, shine, handle, connector
						// nose, lens, rim, head, inner stripe, outer stripe

						break;
					case GreenChestType.Captain:
						// base, center, top paddle, top handle, bottom paddle, bottom handle
						// base, top stripe, rudder, missle base, top fin, bottom fin, left stripe, right stripe
						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								costs.Add(300);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(10);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(15);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(25);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(15);
							}
							if (!FilledPieces.Contains(5)) {
								costs.Add(15);
							}
							if (!FilledPieces.Contains(6)) {
								costs.Add(5);
							}
							if (!FilledPieces.Contains(7)) {
								costs.Add(5);
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								costs.Add(60);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(30);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(20);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(12);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(20);
							}
							if (!FilledPieces.Contains(5)) {
								costs.Add(12);
							}
						}

						break;
					case GreenChestType.Dragon:
						// top, center, left, right
						// top, left center, left left, left right, right center, right right, right left

						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								costs.Add(10);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(35);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(25);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(20);
							}
							if (!FilledPieces.Contains(4)) {
								costs.Add(35);
							}
							if (!FilledPieces.Contains(5)) {
								costs.Add(20);
							}
							if (!FilledPieces.Contains(6)) {
								costs.Add(20);
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								costs.Add(10);
							}
							if (!FilledPieces.Contains(1)) {
								costs.Add(35);
							}
							if (!FilledPieces.Contains(2)) {
								costs.Add(20);
							}
							if (!FilledPieces.Contains(3)) {
								costs.Add(25);
							}
						}

						break;
					case GreenChestType.Sparky:

						if (!FilledPieces.Contains(0)) {
							costs.Add(20);
						}
						if (!FilledPieces.Contains(1)) {
							costs.Add(20);
						}
						if (!FilledPieces.Contains(2)) {
							costs.Add(15);
						}
						if (!FilledPieces.Contains(3)) {
							costs.Add(100);
						}
						// waist, lightning, neck, body

						break;
					case GreenChestType.Devil:
						// head, left eye, right eye, mouth, left horn, right horn, left wing, right wing
						if (!FilledPieces.Contains(0)) {
							costs.Add(60);
						}
						if (!FilledPieces.Contains(1)) {
							costs.Add(5);
						}
						if (!FilledPieces.Contains(2)) {
							costs.Add(5);
						}
						if (!FilledPieces.Contains(3)) {
							costs.Add(20);
						}
						if (!FilledPieces.Contains(4)) {
							costs.Add(15);
						}
						if (!FilledPieces.Contains(5)) {
							costs.Add(15);
						}
						if (!FilledPieces.Contains(6)) {
							costs.Add(35);
						}
						if (!FilledPieces.Contains(7)) {
							costs.Add(35);
						}

						break;
					default:
						throw new Exception();

				}
				costs.Sort();
				if (costs[costs.Count - 1] > 25) {
					costs[costs.Count - 1] = 70;
				}
				
				

				foreach (var i in costs) {
					Cost += i;
				}
			}
			else {
				switch (type) {
					case GreenChestType.Cosmic:
						if (upgrade) {

							///if \((FilledPieces.Contains\([0-9]\)\))
							if (!FilledPieces.Contains(0)) {
								Cost += 20;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(3)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(4)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(5)) {
								Cost += 2;
							}
							// base, rod, orb, wing, handle, base
						}
						else {
							if (!FilledPieces.Contains(0)) { // helmet
								Cost += 20;
							}
							if (!FilledPieces.Contains(2)) { // base
								Cost += 5;
							}
							if (!FilledPieces.Contains(3)) { // left handle
								Cost += 0;
							}
							if (!FilledPieces.Contains(4)) { // right handle
								Cost += 1;
							}
						}

						break;
					case GreenChestType.Arty:
						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 30;
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								Cost += 20;
							}
							if (!FilledPieces.Contains(1)) {
								Cost += 2;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 3;
							}
						}
						// base, stripe, bean, rim
						// bristles, handle, base, purple, pink, blue

						break;
					case GreenChestType.Genius:
						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								Cost += 3;
							}
							if (!FilledPieces.Contains(1)) {
								Cost += 20;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 1;
							}
							if (!FilledPieces.Contains(3)) {
								Cost += 10;
							}
							if (!FilledPieces.Contains(5)) {
								Cost += 5;
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(1)) {
								Cost += 20;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 1;
							}

							if (!FilledPieces.Contains(2) && !FilledPieces.Contains(4)) {
								Cost -= 3;
							}
						}
						// frame, glass, shine, handle, connector
						// nose, lens, rim, head, inner stripe, outer stripe

						break;
					case GreenChestType.Captain:
						// base, center, top paddle, top handle, bottom paddle, bottom handle
						// base, top stripe, rudder, missle base, top fin, bottom fin, left stripe, right stripe
						if (upgrade) {
							if (!FilledPieces.Contains(0)) {
								Cost += 30;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 2;
							}
							if (!FilledPieces.Contains(3)) {
								Cost += 1;
							}
							if (!FilledPieces.Contains(4)) {
								Cost += 2;
							}
							if (!FilledPieces.Contains(5)) {
								Cost += 2;
							}
							if (!FilledPieces.Contains(6)) {
								Cost += 1;
							}
							if (!FilledPieces.Contains(7)) {
								Cost += 1;
							}
						}
						else {
							if (!FilledPieces.Contains(0)) {
								Cost += 10;
							}
							if (!FilledPieces.Contains(1)) {
								Cost += 7;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(4)) {
								Cost += 5;
							}
						}

						break;
					case GreenChestType.Sparky:

						if (!FilledPieces.Contains(0)) {
							Cost += 20;
						}
						if (!FilledPieces.Contains(1)) {
							Cost += 4;
						}
						if (!FilledPieces.Contains(2)) {
							Cost += 2;
						}
						// body, neck, lightning, waist

						break;
					case GreenChestType.Dragon:
						// top, center, left, right
						// top, left center, left left, left right, right center, right right, right left

						if (upgrade) {
							if (!FilledPieces.Contains(1)) {
								Cost += 4;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 4;
							}
							if (!FilledPieces.Contains(3)) {
								Cost += 3;
							}
							if (!FilledPieces.Contains(4)) {
								Cost += 4;
							}
							if (!FilledPieces.Contains(5)) {
								Cost += 2;
							}
							if (!FilledPieces.Contains(6)) {
								Cost += 2;
							}
						}
						else {
							if (!FilledPieces.Contains(1)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(2)) {
								Cost += 5;
							}
							if (!FilledPieces.Contains(3)) {
								Cost += 5;
							}
						}

						break;
					case GreenChestType.Devil:
						// head, left eye, right eye, mouth, left horn, right horn, left wing, right wing
						if (!FilledPieces.Contains(0)) {
							Cost += 15;
						}
						if (!FilledPieces.Contains(3)) {
							Cost += 3;
						}
						if (!FilledPieces.Contains(4)) {
							Cost += 1;
						}
						if (!FilledPieces.Contains(5)) {
							Cost += 1;
						}
						if (!FilledPieces.Contains(6)) {
							Cost += 15;
						}
						if (!FilledPieces.Contains(7)) {
							Cost += 15;
						}

						break;
					default:
						throw new Exception();

						break;
				}
			}

			RunParticles(rng);
		}

		public static void RunParticles(WarioRandom rng) {

			for (int i = 0; i < 160; i++) {
				if (rng.GetRange(6) == 0) {
					rng.GetInteger();
					rng.GetInteger();
					rng.GetInteger();
				}
			}
		}
	}
}
