-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

DROP TABLE activity;
DROP TABLE mascot;
DROP TABLE child;
DROP TABLE parent;
DROP TABLE items;

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
	mascot_id			integer			identity,
	mascot_image		varchar(32),
	child_id			integer			NOT NULL,
	current_hat			varchar(32),
	current_background	varchar(32),
	baseball_hat		bit				default 0,
	beanie				bit				default 0,
	bonnet				bit				default 0,
	bow					bit				default 0,
	bucket_hat			bit				default 0,
	crown				bit				default 0,
	flower				bit				default 0,
	propeller_hat		bit				default 0,
	sombrero			bit				default 0,
	top_hat				bit				default 0,
	beach				bit				default 0,
	city				bit				default 0,
	desert				bit				default 0,
	forest				bit				default 0,
	mountain			bit				default 0,
	ocean				bit				default 0,

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

CREATE TABLE items(
	item_id			int				identity,
	image			varchar(32)		NOT NULL,
	price			int				NOT NULL,
	type			varchar(32)		NOT NULL,

	CONSTRAINT pk_item_id PRIMARY KEY (item_id),
);

INSERT INTO items VALUES
-- hats
('baseball_hat.png', 25, 'Hat'),
('beanie.png', 50, 'Hat'),
('bow.png', 75, 'Hat'),
('bucket_hat.png', 100, 'Hat'),
('bonnet.png', 125, 'Hat'),
('propeller_hat.png', 150, 'Hat'),
('sombrero.png', 175, 'Hat'),
('top_hat.png', 200, 'Hat'),
('flower_crown.png', 250, 'Hat'),
('crown.png', 500, 'Hat'),
-- backgrounds
('beach.png', 150, 'Background'),
('city.png', 150, 'Background'),
('desert.png', 150, 'Background'),
('forest.png', 150, 'Background'),
('mountain.png', 150, 'Background'),
('ocean.png', 150, 'Background');

Update Mascot Set baseball_hat= 1, beanie = 1, bonnet = 1, bow= 1, bucket_hat= 1, crown=1, flower= 1, propeller_hat= 1, sombrero= 1, top_hat= 1, beach= 1, city= 1, desert= 1, forest= 1, mountain= 1, ocean = 1 where child_id =1;