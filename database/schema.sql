-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

CREATE TABLE parent (
	parent_id		integer			identity,
	first_name		varchar(64)		NOT NULL,
	last_name		varchar(64)		NOT NULL,
	email			varchar(64)		unique NOT NULL,
	p_word			varchar(32)		NOT NULL,

	CONSTRAINT pk_parent_user_id PRIMARY KEY (parent_id),
);

CREATE TABLE child (
	child_id		integer			identity,
	parent_id		integer			NOT NULL,
	username		varchar(64)		unique NOT NULL,
	first_name		varchar(64)		NOT NULL,
	steps			integer			default 0,
	active_minutes	integer			default 0,
	p_word			varchar(64)		NOT NULL,

	CONSTRAINT pk_child_child_id PRIMARY KEY (child_id),
	CONSTRAINT fk_child_parent_id FOREIGN KEY (parent_id) REFERENCES parent (parent_id),
);