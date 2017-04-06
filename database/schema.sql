-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

DROP TABLE parent;
DROP TABLE child;
DROP TABLE mascot;
DROP TABLE activity;


CREATE TABLE parent (
	parent_id		integer			identity,
	first_name		varchar(64)		NOT NULL,
	last_name		varchar(64)		NOT NULL,
	email			varchar(64)		unique NOT NULL,
	p_word			varchar(32)		NOT NULL,
	salt			varchar(32)		NOT NULL,

	CONSTRAINT pk_parent_user_id PRIMARY KEY (parent_id),
);

CREATE TABLE child (
	child_id		integer			identity,
	parent_id		integer			NOT NULL,
	username		varchar(64)		unique NOT NULL,
	first_name		varchar(64)		NOT NULL,
	seconds			integer			default 0,
	carrots			integer			default 0,
	p_word			varchar(32)		NOT NULL,
	salt			varchar(32)		NOT NULL,

	CONSTRAINT pk_child_child_id PRIMARY KEY (child_id),
	CONSTRAINT fk_child_parent_id FOREIGN KEY (parent_id) REFERENCES parent (parent_id),
);

CREATE TABLE mascot (
	mascot_id		integer			identity,
	mascot_image	varchar(32),
	child_id		integer			NOT NULL,
	current_hat		varchar(32),
	baseball_hat	bit				default 0,
	beanie			bit				default 0,
	bonnet			bit				default 0,
	bucket_hat		bit				default 0,
	crown			bit				default 0,
	flower			bit				default 0,
	propeller_hat	bit				default 0,
	sombrero		bit				default 0,
	top_hat			bit				default 0,

	CONSTRAINT pk_mascot_mascot_id PRIMARY KEY (mascot_id),
	CONSTRAINT fk_mascot_child_id FOREIGN KEY (child_id) REFERENCES child (child_id),
);

CREATE TABLE activity(
	activity_id		integer			identity,
	child_id		integer			NOT NULL,
	activity_date	datetime		NOT NULL,
	seconds			int				default 0,
	carrots			int				default 0,

	CONSTRAINT pk_activity_id PRIMARY KEY (activity_id),
	CONSTRAINT fk_activity_child_id FOREIGN KEY (child_id) REFERENCES child (child_id),
);