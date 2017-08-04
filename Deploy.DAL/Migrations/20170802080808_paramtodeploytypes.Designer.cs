using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Deploy.DAL;

namespace Deploy.DAL.Migrations
{
    [DbContext(typeof(DeployDBContext))]
    [Migration("20170802080808_paramtodeploytypes")]
    partial class paramtodeploytypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Deploy.DAL.DeployChoices", b =>
                {
                    b.Property<int>("DeployChoicesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseOption");

                    b.Property<bool>("Datadisk");

                    b.Property<string>("DeployCode");

                    b.Property<string>("DeployFile");

                    b.Property<string>("DeployName");

                    b.Property<bool>("Domain");

                    b.Property<string>("ParamsFile");

                    b.Property<string>("Size");

                    b.Property<bool>("Solution");

                    b.HasKey("DeployChoicesID");

                    b.ToTable("DeployChoices");
                });

            modelBuilder.Entity("Deploy.DAL.DeployList", b =>
                {
                    b.Property<int>("DeployListID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeployName");

                    b.Property<string>("DeployType");

                    b.Property<string>("DeployValue");

                    b.HasKey("DeployListID");

                    b.ToTable("DeployList");
                });

            modelBuilder.Entity("Deploy.DAL.DeployParam", b =>
                {
                    b.Property<int>("DeployParamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ParamToolTip");

                    b.Property<string>("ParameterDeployType");

                    b.Property<string>("ParameterName");

                    b.Property<string>("ParameterType");

                    b.Property<int?>("TennantParamID");

                    b.HasKey("DeployParamID");

                    b.HasIndex("TennantParamID");

                    b.ToTable("DeployParms");
                });

            modelBuilder.Entity("Deploy.DAL.DeployType", b =>
                {
                    b.Property<int>("DeployTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AzureDeployName");

                    b.Property<string>("DeployCode");

                    b.Property<string>("DeployFile");

                    b.Property<string>("DeployName");

                    b.Property<string>("DeployResult");

                    b.Property<string>("DeploySaved");

                    b.Property<string>("DeployState");

                    b.Property<string>("ParamsFile");

                    b.Property<string>("ResourceGroupName");

                    b.Property<int>("TennantID");

                    b.HasKey("DeployTypeID");

                    b.HasIndex("TennantID");

                    b.ToTable("DeployTypes");
                });

            modelBuilder.Entity("Deploy.DAL.Queue", b =>
                {
                    b.Property<int>("QueueID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DeployName");

                    b.Property<int>("DeployTypeID");

                    b.Property<int>("Order");

                    b.Property<int>("TennantID");

                    b.Property<string>("TennantName");

                    b.Property<string>("azuredeploy");

                    b.Property<bool>("resource");

                    b.Property<string>("resourcegroup");

                    b.Property<string>("status");

                    b.Property<string>("subscriptionID");

                    b.HasKey("QueueID");

                    b.HasIndex("DeployTypeID");

                    b.ToTable("Queue");
                });

            modelBuilder.Entity("Deploy.DAL.Tennant", b =>
                {
                    b.Property<int>("TennantID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AzureClientID");

                    b.Property<string>("AzureClientSecret");

                    b.Property<string>("AzureSubscriptionID");

                    b.Property<string>("AzureTennantID");

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("ResourceGroupLocation")
                        .IsRequired();

                    b.Property<string>("ResourceGroupName");

                    b.Property<string>("TennantName")
                        .IsRequired();

                    b.HasKey("TennantID");

                    b.ToTable("Tennants");
                });

            modelBuilder.Entity("Deploy.DAL.TennantParam", b =>
                {
                    b.Property<int>("TennantParamID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DeployParamID");

                    b.Property<int>("DeployTypeID");

                    b.Property<string>("ParamName");

                    b.Property<string>("ParamToolTip");

                    b.Property<string>("ParamType");

                    b.Property<string>("ParamValue")
                        .IsRequired();

                    b.HasKey("TennantParamID");

                    b.HasIndex("DeployTypeID");

                    b.ToTable("TennantParams");
                });

            modelBuilder.Entity("Deploy.DAL.DeployParam", b =>
                {
                    b.HasOne("Deploy.DAL.TennantParam")
                        .WithMany("DeployParams")
                        .HasForeignKey("TennantParamID");
                });

            modelBuilder.Entity("Deploy.DAL.DeployType", b =>
                {
                    b.HasOne("Deploy.DAL.Tennant", "Tennants")
                        .WithMany("DeployTypes")
                        .HasForeignKey("TennantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Deploy.DAL.Queue", b =>
                {
                    b.HasOne("Deploy.DAL.DeployType", "DeployTypes")
                        .WithMany()
                        .HasForeignKey("DeployTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Deploy.DAL.TennantParam", b =>
                {
                    b.HasOne("Deploy.DAL.DeployType", "DeployTypes")
                        .WithMany("TennantParams")
                        .HasForeignKey("DeployTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
