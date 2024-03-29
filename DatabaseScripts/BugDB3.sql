USE [master]
GO

/****** Object:  Database [BugDB]    Script Date: 01/01/2010 23:31:12 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'BugDB')
DROP DATABASE [BugDB]
GO

/****** Object:  Database [BugDB]    Script Date: 01/01/2010 23:37:34 ******/
CREATE DATABASE [BugDB] ON  PRIMARY 
( NAME = N'BugDB', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\BugDB.mdf' , SIZE = 2240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BugDB_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\BugDB_log.LDF' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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

/****** Object:  Table [dbo].[Types]    Script Date: 01/01/2010 23:37:36 ******/
/*SET ANSI_NULLS ON
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
GO*/
/****** Object:  Table [dbo].[Applications]    Script Date: 01/01/2010 23:37:36 ******/
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
/****** Object:  Table [dbo].[Statuses]    Script Date: 01/01/2010 23:37:36 ******/
/*SET ANSI_NULLS ON
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
GO*/

/****** Object:  Table [dbo].[Staff]    Script Date: 01/01/2010 23:37:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[person_id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Summaries]    Script Date: 01/01/2010 23:37:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Summaries]
(
	[summary_id]	[int] IDENTITY(1,1) NOT NULL,
	[summary_text]	[varchar](160) NOT NULL,
		CONSTRAINT [PK_Summaries] PRIMARY KEY CLUSTERED 
		(
			[summary_id] ASC
		) WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Summaries_Text] ON [Summaries] 
(
	[summary_text] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Summaries', @level2type=N'COLUMN',@level2name=N'summary_text'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short descriptions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Summaries'
GO
/****** Object:  Table [dbo].[Bugs]    Script Date: 01/01/2010 23:37:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs]
(
	[bug_number]		[int] NOT NULL,
	[recent_revision]	[int] NOT NULL,
		CONSTRAINT [PK_Bugs] PRIMARY KEY CLUSTERED 
		(
			[bug_number] ASC
		) WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'bug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'most recent revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'recent_revision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bugs list (only one record for one bug). Contains most recent bug state.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs'
GO
/****** Object:  Table [dbo].[Revisions]    Script Date: 01/01/2010 23:37:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Revisions]
(
	[bug_number]		[int] NOT NULL,
	[revision]			[int] NOT NULL,
	[date]				[datetime] NOT NULL,
	[bug_type]			[int] NOT NULL,
	[app_id]			[int] NOT NULL,
	[module_id]			[int] NULL,
	[submodule_id]		[int] NULL,
	[status]			[int] NOT NULL,
	[found_release_id]	[int] NULL,
	[target_release_id] [int] NULL,
	[severity]			[int] NULL,
	[priority]			[int] NULL,
	[contributor_id]	[int] NOT NULL,
	[leader_id]			[int] NULL,
	[developer_id]		[int] NULL,
	[qa_id]				[int] NULL,
	[summary_id]		[int] NOT NULL,
		CONSTRAINT [PK_Revisions] PRIMARY KEY CLUSTERED 
		(
			[bug_number] ASC,
			[revision] ASC
		) WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'bug_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'date of revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'type of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'bug_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'app_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module in application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'module_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule in module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'submodule_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'status of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug was found' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'found_release_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug is planned to be fixed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'target_release_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'severity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'severity'
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
/****** Object:  Table [dbo].[Modules]    Script Date: 01/01/2010 23:37:36 ******/
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
/****** Object:  Table [dbo].[Releases]    Script Date: 01/01/2010 23:37:36 ******/
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
/****** Object:  Table [dbo].[Submodules]    Script Date: 01/01/2010 23:37:36 ******/
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
ALTER TABLE [dbo].[Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Revisions] FOREIGN KEY([bug_number], [recent_revision])
REFERENCES [dbo].[Revisions] ([bug_number], [revision])
GO
ALTER TABLE [dbo].[Bugs] CHECK CONSTRAINT [FK_Bugs_Revisions]
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
/*ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Statuses] FOREIGN KEY([status_id])
REFERENCES [dbo].[Statuses] ([status_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Statuses]
GO*/
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
/*ALTER TABLE [dbo].[Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Types] FOREIGN KEY([bug_type_id])
REFERENCES [dbo].[Types] ([type_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Revisions] CHECK CONSTRAINT [FK_Revisions_Types]
GO*/
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


-- ===============================================
-- Stored procedure for adding revisions.
-- Correctly processes Bugs and Summaries tables.
-- ===============================================
-- Controls whether non-ANSI equality comparisons with NULLs are allowed
SET ANSI_NULLS ON
GO
-- Controls whether strings within double quotes are interpreted 
-- as object identifiers (e.g., table or column references)
SET QUOTED_IDENTIFIER ON
GO
-- SET XACT_ABORT, SET CURSOR_CLOSE_ON_COMMIT, SET TEXTSIZE, SET IMPLICIT_TRANSACTIONS

CREATE Procedure Revision_Create
	@bug_number			int,
	@revision			int,
	@date				datetime,
	@bug_type			int, 
	@app_id				int,
	@module_id			int,
	@submodule_id		int,
    @status				int,
    @found_release_id	int, 
    @target_release_id	int,
    @severity			int,
    @priority			int,
    @contributor_id		int,
    @leader_id			int,
    @developer_id		int,
    @qa_id				int,
    @summary_text		varchar(160)
AS
	DECLARE @summary_id int
	DECLARE @recent_revision int
	DECLARE @summary_text_db varchar(160)

	DECLARE o_curs CURSOR LOCAL FOR
		SELECT summary_id, summary_text
		FROM Summaries 
		WHERE summary_text = @summary_text
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- First process summary.
	-- Summaries are stored in separate table as unique strings.
	-- So, if it exists - then use, overwise - create new one.
	SELECT @summary_id=summary_id, @summary_text_db=summary_text
	FROM (
		SELECT TOP 1 summary_id, summary_text 
		FROM Summaries 
		WHERE summary_text = @summary_text) Summaries
	IF @summary_id IS NULL BEGIN
		SET @summary_text_db = @summary_text
		-- Insert new summary
		INSERT INTO Summaries (summary_text) VALUES (@summary_text_db)
		SET @summary_id = Cast(SCOPE_IDENTITY() as int) 
	END
	
	-- Then insert revision into Revisions table
	INSERT INTO Revisions
			   ([bug_number], [revision], [date], [bug_type], 
				[app_id], [module_id], [submodule_id],
				[status],[found_release_id],[target_release_id],
				[severity],[priority],[contributor_id],[leader_id],[developer_id],[qa_id],
				[summary_id])
	VALUES
			   (@bug_number, @revision, @date, @bug_type, 
				@app_id, @module_id, @submodule_id,
				@status, @found_release_id, @target_release_id,
				@severity, @priority, 
				@contributor_id, @leader_id, @developer_id, @qa_id, 
				@summary_id)
	            
	-- Insert or update record in Bugs table
	-- TODO: Could be reworked into TRIGGER
	SELECT @recent_revision=recent_revision
	FROM (
		SELECT recent_revision
		FROM Bugs 
		WHERE bug_number = @bug_number) Bugs
	IF (@recent_revision IS NULL) BEGIN
		SET @recent_revision = @revision
		-- Insert new bug
		INSERT INTO Bugs (bug_number, recent_revision) VALUES (@bug_number, @recent_revision)
	END ELSE IF (@revision > @recent_revision) BEGIN
		UPDATE Bugs SET recent_revision=@revision WHERE bug_number=@bug_number
	END
END
GO

-- ===============================================
-- Cleans storage.
-- Deletes data from all tables.
-- http://www.nerdymusings.com/LPMArticle.asp?ID=9#download
-- ===============================================
-- Controls whether non-ANSI equality comparisons with NULLs are allowed
SET ANSI_NULLS OFF
GO
-- Controls whether strings within double quotes are interpreted 
-- as object identifiers (e.g., table or column references)
SET QUOTED_IDENTIFIER OFF
GO
-- SET XACT_ABORT, SET CURSOR_CLOSE_ON_COMMIT, SET TEXTSIZE, SET IMPLICIT_TRANSACTIONS

CREATE Procedure Storage_Clean
AS
BEGIN
	DECLARE @ReturnCode int,
		@ErrorCode int,
		@RowCount int,
		@InTrans tinyint,
		@Message varchar(200)
	SELECT @ReturnCode = 0,
		@ErrorCode = 0,
		@RowCount = 0,
		@InTrans = 0,
		@Message = 'Storage_Clean: '

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/* Begin a transaction to wrap operations that must be completed together */
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRANSACTION
	SET @InTrans = 1
	
	/* Perform first operation. */
	DELETE FROM Bugs;			
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Bugs'
		GOTO ProcError
	END
	
	DELETE FROM Revisions;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Revisions'
		GOTO ProcError
	END

	DELETE FROM Submodules;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Submodules'
		GOTO ProcError
	END
	
	DELETE FROM Modules;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Modules'
		GOTO ProcError
	END
	
	DELETE FROM Releases;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Releases'
		GOTO ProcError
	END
	
	DELETE FROM Applications;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Applications'
		GOTO ProcError
	END
	
	DELETE FROM Staff;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Staff'
		GOTO ProcError
	END
	
	DELETE FROM Summaries;
	SELECT @ErrorCode = @@ERROR
	IF @ErrorCode <> 0 BEGIN
		SET @ReturnCode = 101
		SET @Message = @Message + 'Unable to clean Summaries'
		GOTO ProcError
	END
	
ProcExit:
	/* Commit or abort the transaction. */
	IF @InTrans = 1 BEGIN
		IF @ReturnCode = 0 BEGIN
			COMMIT TRANSACTION
		END ELSE BEGIN
			ROLLBACK TRANSACTION
		END
	END
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	RETURN(@ReturnCode)
ProcError:
	RAISERROR(@Message,16,1)
	GOTO ProcExit
END
GO
