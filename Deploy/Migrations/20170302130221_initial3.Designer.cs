using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Deploy.DAL;

namespace Deploy.Migrations
{
    [DbContext(typeof(DeployDBContext))]
    [Migration("20170302130221_initial3")]
    partial class initial3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Deploy.Models.DeployParam", b =>
                {
                    b.Property<int>("DeployParamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ParameterName");

                    b.Property<string>("ParameterType");

                    b.Property<int?>("TennantParamID");

                    b.HasKey("DeployParamID");

                    b.HasIndex("TennantParamID");

                    b.ToTable("DeployParms");
                });

            modelBuilder.Entity("Deploy.Models.DeployType", b =>
                {
                    b.Property<int>("DeployTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeployName");

                    b.Property<int>("TennantID");

                    b.Property<int>("TennantParamID");

                    b.HasKey("DeployTypeID");

                    b.HasIndex("TennantID");

                    b.ToTable("DeployTypes");
                });

            modelBuilder.Entity("Deploy.Models.Tennant", b =>
                {
                    b.Property<int>("TennantID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("TennantName")
                        .IsRequired();

                    b.HasKey("TennantID");

                    b.ToTable("Tennants");
                });

            modelBuilder.Entity("Deploy.Models.TennantParam", b =>
                {
                    b.Property<int>("TennantParamID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeployParamID");

                    b.Property<int>("DeployTypeID");

                    b.Property<string>("ParamValue");

                    b.HasKey("TennantParamID");

                    b.HasIndex("DeployTypeID");

                    b.ToTable("TennantParams");
                });

            modelBuilder.Entity("Deploy.Models.DeployParam", b =>
                {
                    b.HasOne("Deploy.Models.TennantParam")
                        .WithMany("DeployParams")
                        .HasForeignKey("TennantParamID");
                });

            modelBuilder.Entity("Deploy.Models.DeployType", b =>
                {
                    b.HasOne("Deploy.Models.Tennant", "Tennants")
                        .WithMany("DeployTypes")
                        .HasForeignKey("TennantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Deploy.Models.TennantParam", b =>
                {
                    b.HasOne("Deploy.Models.DeployType", "DeployTypes")
                        .WithMany("TennantParams")
                        .HasForeignKey("DeployTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
