using System;
using Rocket.API;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Blitz
{
	public class BlitzConfiguration : IRocketPluginConfiguration
	{
		[XmlArrayItem (ElementName = "PlayerData")]
		public List<PlayerData> Players;

		[XmlArrayItem (ElementName = "Match")]
		public List<Match> Matches;

		public IRocketPluginConfiguration DefaultConfiguration {
			get {
				return new BlitzConfiguration {
					Players = new List<PlayerData> {
						new PlayerData ("76561198078287506", "GENERAL", "Rifleman")
					},

					Matches = new List<Match> {
						new Match {
							Name = "Capture Oulton's castle",
							Lobby = new Lobby(197, 33, -831),
							Objective = new Objective {
								Type = EObjectiveType.CAPTURE.ToString(),
								Region = new CapturePoint {
									Radius = 4,
									x = 197,
									y = 43,
									z = -804,
									MinimumPlayers = 1,
									MimimumTime = 2f
								}
							},
							Spawns = new List<Spawn> {
								new Spawn(253, 31, -822, "Default", "red"),
								new Spawn(197, 33, -802, "Default", "blue")
							},
							Units = new List<Unit> {
								new Unit {
									Name = "Rifleman",
									Default = true,
									Loadout = new List<UnitItem> {
										new UnitItem (101, 1),
										new UnitItem (103, 3),
										new UnitItem (44, 1)
									}
								}
							},
							MatchTime = 60,
							Lives = 1
						}
					}
				};
			}
		}
	}
}

