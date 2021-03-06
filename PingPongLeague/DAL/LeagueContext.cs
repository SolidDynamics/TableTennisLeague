﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PingPongLeague.Models;

namespace PingPongLeague.DAL
{
	public class LeagueContext : DbContext
	{
		public LeagueContext() : base("LeagueContext")
		{
		}

		public DbSet<Match> Matches { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<MatchParticipation> MatchParticipations { get; set; }
		public DbSet<Competition> Competitions { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
