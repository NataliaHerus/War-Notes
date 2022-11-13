-- CREATE DATABASE WarNotes;

CREATE TABLE IF NOT EXISTS "Users"
(
	"Id" SERIAL PRIMARY KEY,
	"FirstName" VARCHAR(50) NOT NULL,
	"LastName" VARCHAR(50) NOT NULL,
	"Email" VARCHAR NOT NULL,
	"Password" VARCHAR NOT NULL,
	"Role" INT NOT NULL,
	"IsBlocked" BOOL NOT NULL
);

CREATE TABLE IF NOT EXISTS "Categories"
(
	"Id" SERIAL PRIMARY KEY,
	"CategoryName" VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS "Articles"
(
	"Id" SERIAL PRIMARY KEY,
	"CreatedAt" timestamp default NULL,
	"Title" text default NULL,
	"Text" text default NULL,
	"CategoryId" INT NOT NULL,
	CONSTRAINT fk_articles_category FOREIGN KEY ("CategoryId") REFERENCES
	"Categories"("Id")
);

CREATE TABLE IF NOT EXISTS "SavedArticles"
(
	"Id" SERIAL PRIMARY KEY,
	"UserId" INT NOT NULL,
	"ArticleId" INT NOT NULL
);

CREATE TABLE IF NOT EXISTS "LikedArticles"
(
	"Id" SERIAL PRIMARY KEY,
	"UserId" INT NOT NULL,
	"ArticleId" INT NOT NULL
);

