USE [BugDB]
GO
/****** Object:  Table [Types]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Types]') AND type in (N'U'))
BEGIN
CREATE TABLE [Types](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[type_title] [varchar](12) NOT NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Types', N'COLUMN',N'type_title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'title for type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Types', @level2type=N'COLUMN',@level2name=N'type_title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Types', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'types of records in BugDB' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Types'
GO
/****** Object:  Table [Applications]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Applications]') AND type in (N'U'))
BEGIN
CREATE TABLE [Applications](
	[app_id] [int] IDENTITY(1,1) NOT NULL,
	[app_title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[app_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Applications', N'COLUMN',N'app_title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'title of application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'app_title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Applications', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'applications list' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications'
GO
/****** Object:  Table [Statuses]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Statuses]') AND type in (N'U'))
BEGIN
CREATE TABLE [Statuses](
	[status_id] [int] IDENTITY(1,1) NOT NULL,
	[status_title] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Statuses', N'COLUMN',N'status_title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'title of status' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Statuses', @level2type=N'COLUMN',@level2name=N'status_title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Statuses', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug statuses' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Statuses'
GO
/****** Object:  Table [Severities]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Severities]') AND type in (N'U'))
BEGIN
CREATE TABLE [Severities](
	[severity_id] [int] NOT NULL,
	[severity_title] [varchar](30) NOT NULL CONSTRAINT [DF_Severities_severity_title]  DEFAULT ('title of severity'),
 CONSTRAINT [PK_Severities] PRIMARY KEY CLUSTERED 
(
	[severity_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Severities', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug severities' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Severities'
GO
/****** Object:  Table [Staff]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Staff]') AND type in (N'U'))
BEGIN
CREATE TABLE [Staff](
	[person_id] [int] NOT NULL,
	[person_login] [varchar](30) NOT NULL,
	[person_title] [varchar](50) NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[person_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Staff', N'COLUMN',N'person_login'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person''s login' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Staff', @level2type=N'COLUMN',@level2name=N'person_login'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Staff', N'COLUMN',N'person_title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person''s full name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Staff', @level2type=N'COLUMN',@level2name=N'person_title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Staff', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'personnel' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Staff'
GO
/****** Object:  Table [Summaries]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Summaries]') AND type in (N'U'))
BEGIN
CREATE TABLE [Summaries](
	[summary_id] [int] NOT NULL,
	[summary_text] [varchar](160) NOT NULL,
 CONSTRAINT [PK_Summaries] PRIMARY KEY CLUSTERED 
(
	[summary_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Summaries', N'COLUMN',N'summary_text'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Summaries', @level2type=N'COLUMN',@level2name=N'summary_text'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Summaries', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short descriptions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Summaries'
GO
/****** Object:  Table [Bugs]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Bugs]') AND type in (N'U'))
BEGIN
CREATE TABLE [Bugs](
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'bug_number'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'bug_number'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'recent_revision'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'most recent revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'recent_revision'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'date'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of the revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'date'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'bug_type_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'type of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'bug_type_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'app_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'app_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'module_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module in application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'module_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'submodule_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule in module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'submodule_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'status_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'status of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'status_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'found_release_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug was found' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'found_release_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'target_release_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug is planned to be fixed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'target_release_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'severity_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'severity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'severity_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'priority'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'priority' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'priority'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'contributor_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug creator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'contributor_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'leader_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person who assigned bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'leader_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'developer_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person responsible for fixing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'developer_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'qa_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tester responsible for testing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'qa_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', N'COLUMN',N'summary_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs', @level2type=N'COLUMN',@level2name=N'summary_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Bugs', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bugs list (only one record for one bug). Contains most recent bug state.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bugs'
GO
/****** Object:  Table [Revisions]    Script Date: 01/01/2010 23:09:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Revisions]') AND type in (N'U'))
BEGIN
CREATE TABLE [Revisions](
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'bug_number'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'bug number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'bug_number'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'date'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'date of revision' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'date'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'bug_type_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'type of record' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'bug_type_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'app_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'app_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'module_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module in application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'module_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'submodule_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule in module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'submodule_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'status_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'status of bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'status_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'found_release_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug was found' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'found_release_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'target_release_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'release in which bug is planned to be fixed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'target_release_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'severity_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'severity' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'severity_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'priority'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'priority' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'priority'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'contributor_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'contributor_id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'contributor_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'leader_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person who assigned bug' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'leader_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'developer_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'person responsible for fixing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'developer_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'qa_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tester responsible for testing' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'qa_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', N'COLUMN',N'summary_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'short description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions', @level2type=N'COLUMN',@level2name=N'summary_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Revisions', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'all bug revisions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Revisions'
GO
/****** Object:  Table [Modules]    Script Date: 01/01/2010 23:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [Modules](
	[module_id] [int] IDENTITY(1,1) NOT NULL,
	[app_id] [int] NOT NULL,
	[module_title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[module_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Modules', N'COLUMN',N'app_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Application for which module is defined' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'app_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Modules', N'COLUMN',N'module_title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules', @level2type=N'COLUMN',@level2name=N'module_title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Modules', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'modules for applications' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Modules'
GO
/****** Object:  Table [Releases]    Script Date: 01/01/2010 23:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Releases]') AND type in (N'U'))
BEGIN
CREATE TABLE [Releases](
	[release_id] [int] IDENTITY(1,1) NOT NULL,
	[app_id] [int] NOT NULL,
	[release_title] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Releases] PRIMARY KEY CLUSTERED 
(
	[release_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Releases', N'COLUMN',N'app_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Releases', @level2type=N'COLUMN',@level2name=N'app_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Releases', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'application releases' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Releases'
GO
/****** Object:  Table [Submodules]    Script Date: 01/01/2010 23:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Submodules]') AND type in (N'U'))
BEGIN
CREATE TABLE [Submodules](
	[submodule_id] [int] IDENTITY(1,1) NOT NULL,
	[module_id] [int] NOT NULL,
	[submodule_title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Submodules] PRIMARY KEY CLUSTERED 
(
	[submodule_id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Submodules', N'COLUMN',N'module_id'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'module' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Submodules', @level2type=N'COLUMN',@level2name=N'module_id'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Submodules', N'COLUMN',N'submodule_title'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodule title' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Submodules', @level2type=N'COLUMN',@level2name=N'submodule_title'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Submodules', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'submodules for modules' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Submodules'
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Applications]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Applications] FOREIGN KEY([app_id])
REFERENCES [Applications] ([app_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Applications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Modules]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Modules] FOREIGN KEY([module_id])
REFERENCES [Modules] ([module_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Modules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Releases_Found]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Releases_Found] FOREIGN KEY([found_release_id])
REFERENCES [Releases] ([release_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Releases_Found]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Releases_Target]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Releases_Target] FOREIGN KEY([target_release_id])
REFERENCES [Releases] ([release_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Releases_Target]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Revisions]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Revisions] FOREIGN KEY([bug_number], [recent_revision])
REFERENCES [Revisions] ([bug_number], [revision])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Revisions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Severities]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Severities] FOREIGN KEY([severity_id])
REFERENCES [Severities] ([severity_id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Severities]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Staff_Contributor]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_Contributor] FOREIGN KEY([contributor_id])
REFERENCES [Staff] ([person_id])
ON UPDATE CASCADE
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_Contributor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Staff_Developer]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_Developer] FOREIGN KEY([developer_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_Developer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Staff_Leader]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_Leader] FOREIGN KEY([leader_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_Leader]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Staff_QA]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Staff_QA] FOREIGN KEY([qa_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Staff_QA]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Statuses]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Statuses] FOREIGN KEY([status_id])
REFERENCES [Statuses] ([status_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Statuses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Submodules]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Submodules] FOREIGN KEY([submodule_id])
REFERENCES [Submodules] ([submodule_id])
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Submodules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Summaries]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Summaries] FOREIGN KEY([summary_id])
REFERENCES [Summaries] ([summary_id])
ON UPDATE CASCADE
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Summaries]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bugs_Types]') AND parent_object_id = OBJECT_ID(N'[Bugs]'))
ALTER TABLE [Bugs]  WITH CHECK ADD  CONSTRAINT [FK_Bugs_Types] FOREIGN KEY([bug_type_id])
REFERENCES [Types] ([type_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Bugs] CHECK CONSTRAINT [FK_Bugs_Types]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Applications]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Applications] FOREIGN KEY([app_id])
REFERENCES [Applications] ([app_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Applications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Modules]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Modules] FOREIGN KEY([module_id])
REFERENCES [Modules] ([module_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Modules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Releases_Found]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Releases_Found] FOREIGN KEY([found_release_id])
REFERENCES [Releases] ([release_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Releases_Found]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Releases_Target]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Releases_Target] FOREIGN KEY([target_release_id])
REFERENCES [Releases] ([release_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Releases_Target]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Severities]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Severities] FOREIGN KEY([severity_id])
REFERENCES [Severities] ([severity_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Severities]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Staff_Contributor]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_Contributor] FOREIGN KEY([contributor_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_Contributor]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Staff_Developer]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_Developer] FOREIGN KEY([developer_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_Developer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Staff_Leader]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_Leader] FOREIGN KEY([leader_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_Leader]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Staff_QA]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Staff_QA] FOREIGN KEY([qa_id])
REFERENCES [Staff] ([person_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Staff_QA]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Statuses]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Statuses] FOREIGN KEY([status_id])
REFERENCES [Statuses] ([status_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Statuses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Submodules]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Submodules] FOREIGN KEY([submodule_id])
REFERENCES [Submodules] ([submodule_id])
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Submodules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Summaries]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Summaries] FOREIGN KEY([summary_id])
REFERENCES [Summaries] ([summary_id])
ON UPDATE CASCADE
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Summaries]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Revisions_Types]') AND parent_object_id = OBJECT_ID(N'[Revisions]'))
ALTER TABLE [Revisions]  WITH CHECK ADD  CONSTRAINT [FK_Revisions_Types] FOREIGN KEY([bug_type_id])
REFERENCES [Types] ([type_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Revisions] CHECK CONSTRAINT [FK_Revisions_Types]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Modules_Applications]') AND parent_object_id = OBJECT_ID(N'[Modules]'))
ALTER TABLE [Modules]  WITH CHECK ADD  CONSTRAINT [FK_Modules_Applications] FOREIGN KEY([app_id])
REFERENCES [Applications] ([app_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Modules] CHECK CONSTRAINT [FK_Modules_Applications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Releases_Applications]') AND parent_object_id = OBJECT_ID(N'[Releases]'))
ALTER TABLE [Releases]  WITH CHECK ADD  CONSTRAINT [FK_Releases_Applications] FOREIGN KEY([app_id])
REFERENCES [Applications] ([app_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Releases] CHECK CONSTRAINT [FK_Releases_Applications]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Submodules_Modules]') AND parent_object_id = OBJECT_ID(N'[Submodules]'))
ALTER TABLE [Submodules]  WITH CHECK ADD  CONSTRAINT [FK_Submodules_Modules] FOREIGN KEY([module_id])
REFERENCES [Modules] ([module_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Submodules] CHECK CONSTRAINT [FK_Submodules_Modules]
