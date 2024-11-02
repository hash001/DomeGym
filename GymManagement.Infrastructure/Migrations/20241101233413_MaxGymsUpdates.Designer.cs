﻿// <auto-generated />
using System;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GymManagement.Infrastructure.Migrations
{
    [DbContext(typeof(GymManagementDbContext))]
    [Migration("20241101233413_MaxGymsUpdates")]
    partial class MaxGymsUpdates
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("GymManagement.Domain.Gyms.Gym", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("TEXT");

                    b.Property<int>("_maxRooms")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxRooms");

                    b.Property<string>("_roomIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("RoomIds");

                    b.Property<string>("_trainerIds")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("TrainerIds");

                    b.HasKey("Id");

                    b.ToTable("Gyms");
                });

            modelBuilder.Entity("GymManagement.Domain.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("TEXT")
                        .HasColumnName("AdminId");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("_maxGyms")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MaxGyms");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
