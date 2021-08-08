using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentAppDAL.Models;

namespace AssignmentAppDAL
{
    public class AssignmentAppDataContext : DbContext
    {
        public AssignmentAppDataContext(DbContextOptions<AssignmentAppDataContext> options) : base(options)
        {

        }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyHearSource> SurveyHearSources { get; set; }
        public DbSet<HearSource> HearSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HearSource>(entity =>
            {
                entity.ToTable("HearSource");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SurveyHearSource>(entity =>
            {
                entity.HasKey(e => new { e.SurveyId, e.HearSourceId })
                    .HasName("PK_Survey_HearSource\\");

                entity.ToTable("Survey_HearSource");

                entity.HasOne(d => d.HearSource)
                    .WithMany(p => p.SurveyHearSources)
                    .HasForeignKey(d => d.HearSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Survey_HearSource_HearSource");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyHearSources)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Survey_HearSource_Survey");


            });

        
        }

    }
}
