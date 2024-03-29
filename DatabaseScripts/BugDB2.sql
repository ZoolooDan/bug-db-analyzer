USE [master]
GO
/****** Object:  Database [BugDB]    Script Date: 01/01/2010 23:31:12 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'BugDB')
DROP DATABASE [BugDB]

USE [master]
GO
/****** Object:  Database [BugDB]    Script Date: 01/01/2010 23:33:22 ******/
CREATE DATABASE [BugDB] ON  PRIMARY 
( NAME = N'BugDB', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\BugDB.mdf' , SIZE = 2240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BugDB_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\BugDB_log.LDF' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE Cyrillic_General_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'BugDB', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BugDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BugDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BugDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BugDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BugDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BugDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BugDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BugDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BugDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BugDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BugDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BugDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BugDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BugDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BugDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BugDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BugDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BugDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BugDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BugDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BugDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BugDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BugDB] SET  READ_WRITE 
GO
ALTER DATABASE [BugDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BugDB] SET  MULTI_USER 
GO
ALTER DATABASE [BugDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BugDB] SET DB_CHAINING OFF 

USE [BugDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[type_title] [varchar](12) NOT NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'title for type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Types', @level2type=N'COLUMN',@level2name=N'type_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'types of records in BugDB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Types'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[app_id] [int] IDENTITY(1,1) NOT NULL,
	[app_title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[app_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'title of application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'app_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'applications list' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[status_id] [int] IDENTITY(1,1) NOT NULL,
	[status_title] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'title of status' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Statuses', @level2type=N'COLUMN',@level2name=N'status_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug statuses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Statuses'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Severities](
	[severity_id] [int] NOT NULL,
	[severity_title] [varchar](30) NOT NULL CONSTRAINT [DF_Severities_severity_title]  DEFAULT ('title of severity'),
 CONSTRAINT [PK_Severities] PRIMARY KEY CLUSTERED 
(
	[severity_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug severities' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Severities'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[person_id] [int] NOT NULL,
	[person_login] [varchar](30) NOT NULL,
	[person_title] [varchar](50) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[person_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person''s login' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Staff', @level2type=N'COLUMN',@level2name=N'person_login'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person''s full name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Staff', @level2type=N'COLUMN',@level2name=N'person_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'personnel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Staff'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Summaries](
	[summary_id] [int] NOT NULL,
	[summary_text] [varchar](160) NOT NULL,
 CONSTRAINT [PK_Summaries] PRIMARY KEY CLUSTERED 
(
	[summary_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Summaries', @level2type=N'COLUMN',@level2name=N'summary_text'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short descriptions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Summaries'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[bug_number] [int] NOT NULL,
	[recent_revision] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[bug_type_id] [int] NOT NULL,
	[app_id] [int] NOT NULL,
	[module_id] [int] NULL,
	[submodule_id] [int] NULL,
	[status_id] [int] NOT NULL,
	[found_release_id] [int] NULL,
	[target_release_id] [int] NULL,
	[severity_id] [int] NULL,
	[priority] [int] NULL,
	[contributor_id] [int] NOT NULL,
	[leader_id] [int] NULL,
	[developer_id] [int] NULL,
	[qa_id] [int] NULL,
	[summary_id] [int] NOT NULL,
 CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
(
	[bug_number] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'bug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'most recent revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'recent_revision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of the revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'type of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'bug_type_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'app_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module in application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'module_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule in module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'submodule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'status of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'status_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug was found' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'found_release_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug is planned to be fixed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'target_release_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'severity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'severity_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'priority' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug creator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'contributor_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person who assigned bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'leader_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person responsible for fixing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'developer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tester responsible for testing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'qa_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'summary_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bugs list (only one record for one bug). Contains most recent bug state.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Revisions](
	[bug_number] [int] NOT NULL,
	[revision] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[bug_type_id] [int] NOT NULL,
	[app_id] [int] NOT NULL,
	[module_id] [int] NULL,
	[submodule_id] [int] NULL,
	[status_id] [int] NOT NULL,
	[found_release_id] [int] NULL,
	[target_release_id] [int] NULL,
	[severity_id] [int] NULL,
	[priority] [int] NULL,
	[contributor_id] [int] NOT NULL,
	[leader_id] [int] NULL,
	[developer_id] [int] NULL,
	[qa_id] [int] NULL,
	[summary_id] [int] NULL,
 CONSTRAINT [PK_Revisions] PRIMARY KEY CLUSTERED 
(
	[bug_number] ASC,
	[revision] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'bug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'date of revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'type of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'bug_type_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'app_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module in application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'module_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule in module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'submodule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'status of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'status_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug was found' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'found_release_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug is planned to be fixed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'target_release_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'severity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'severity_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'priority' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'contributor_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'contributor_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person who assigned bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'leader_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person responsible for fixing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'developer_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tester responsible for testing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'qa_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'summary_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'all bug revisions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[module_id] [int] IDENTITY(1,1) NOT NULL,
	[app_id] [int] NOT NULL,
	[module_title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[module_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application for which module is defined' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'app_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'module_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'modules for applications' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Releases](
	[release_id] [int] IDENTITY(1,1) NOT NULL,
	[app_id] [int] NOT NULL,
	[release_title] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Releases] PRIMARY KEY CLUSTERED 
(
	[release_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Releases', @level2type=N'COLUMN',@level2name=N'app_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application releases' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Releases'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submodules](
	[submodule_id] [int] IDENTITY(1,1) NOT NULL,
	[module_id] [int] NOT NULL,
	[submodule_title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Submodules] PRIMARY KEY CLUSTERED 
(
	[submodule_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Submodules', @level2type=N'COLUMN',@level2name=N'module_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Submodules', @level2type=N'COLUMN',@level2name=N'submodule_title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodules for modules' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Submodules'
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Applications] FOREIGN KEY([app_id])
REFERENCES [dbo].[Applications] ([app_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Applications]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Modules] FOREIGN KEY([module_id])
REFERENCES [dbo].[Modules] ([module_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Modules]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Releases_Found] FOREIGN KEY([found_release_id])
REFERENCES [dbo].[Releases] ([release_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Releases_Found]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Releases_Target] FOREIGN KEY([target_release_id])
REFERENCES [dbo].[Releases] ([release_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Releases_Target]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Revisions] FOREIGN KEY([bug_number], [recent_revision])
REFERENCES [dbo].[Revisions] ([bug_number], [revision])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Revisions]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Severities] FOREIGN KEY([severity_id])
REFERENCES [dbo].[Severities] ([severity_id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Severities]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_Contributor] FOREIGN KEY([contributor_id])
REFERENCES [dbo].[Staff] ([person_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_Contributor]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_Developer] FOREIGN KEY([developer_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_Developer]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_Leader] FOREIGN KEY([leader_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_Leader]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_QA] FOREIGN KEY([qa_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_QA]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Statuses] FOREIGN KEY([status_id])
REFERENCES [dbo].[Statuses] ([status_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Statuses]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Submodules] FOREIGN KEY([submodule_id])
REFERENCES [dbo].[Submodules] ([submodule_id])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Submodules]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Summaries] FOREIGN KEY([summary_id])
REFERENCES [dbo].[Summaries] ([summary_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Summaries]
GO
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Types] FOREIGN KEY([bug_type_id])
REFERENCES [dbo].[Types] ([type_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Types]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Applications] FOREIGN KEY([app_id])
REFERENCES [dbo].[Applications] ([app_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Applications]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Modules] FOREIGN KEY([module_id])
REFERENCES [dbo].[Modules] ([module_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Modules]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Releases_Found] FOREIGN KEY([found_release_id])
REFERENCES [dbo].[Releases] ([release_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Releases_Found]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Releases_Target] FOREIGN KEY([target_release_id])
REFERENCES [dbo].[Releases] ([release_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Releases_Target]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Severities] FOREIGN KEY([severity_id])
REFERENCES [dbo].[Severities] ([severity_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Severities]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_Contributor] FOREIGN KEY([contributor_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_Contributor]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_Developer] FOREIGN KEY([developer_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_Developer]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_Leader] FOREIGN KEY([leader_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_Leader]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_QA] FOREIGN KEY([qa_id])
REFERENCES [dbo].[Staff] ([person_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_QA]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Statuses] FOREIGN KEY([status_id])
REFERENCES [dbo].[Statuses] ([status_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Statuses]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Submodules] FOREIGN KEY([submodule_id])
REFERENCES [dbo].[Submodules] ([submodule_id])
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Submodules]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Summaries] FOREIGN KEY([summary_id])
REFERENCES [dbo].[Summaries] ([summary_id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Summaries]
GO
ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Types] FOREIGN KEY([bug_type_id])
REFERENCES [dbo].[Types] ([type_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Types]
GO
ALTER TABLE [dbo].[Modules]  WITH CHECK ADD  CONSTRAINT [FK_Modules_Applications] FOREIGN KEY([app_id])
REFERENCES [dbo].[Applications] ([app_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Modules] CHECK CONSTRAINT [FK_Modules_Applications]
GO
ALTER TABLE [dbo].[Releases]  WITH CHECK ADD  CONSTRAINT [FK_Releases_Applications] FOREIGN KEY([app_id])
REFERENCES [dbo].[Applications] ([app_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Releases] CHECK CONSTRAINT [FK_Releases_Applications]
GO
ALTER TABLE [dbo].[Submodules]  WITH CHECK ADD  CONSTRAINT [FK_Submodules_Modules] FOREIGN KEY([module_id])
REFERENCES [dbo].[Modules] ([module_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Submodules] CHECK CONSTRAINT [FK_Submodules_Modules]
