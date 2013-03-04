create table T_EQUIPMENT_LOCATION_MAP
(
	EQUIPEMNTID 		varchar(32),
	IGROUP 				INTEGER,
	IROW 				INTEGER ,
	ICOLUMN 			INTEGER
);

create table T_CONFIG
(
	VKEY 				VARCHAR(32),
	VVALUE				VARCHAR(32)
);

insert into T_CONFIG(VKEY,VVALUE) values('group',1);
insert into T_CONFIG(VKEY,VVALUE) values('row',1);
insert into T_CONFIG(VKEY,VVALUE) values('column',1);

create table T_ROOM_CONFIG
(
	 IGROUP 				INTEGER PRIMARY key
	,IROW					INTEGER
	,ICOLUMN				INTEGER
);
INSERT INTO T_ROOM_CONFIG(IGROUP,IROW,ICOLUMN) VALUES(1,1,1);
INSERT INTO T_ROOM_CONFIG(IGROUP,IROW,ICOLUMN) VALUES(2,1,1);
INSERT INTO T_ROOM_CONFIG(IGROUP,IROW,ICOLUMN) VALUES(3,1,1);
SELECT IGROUP,IROW,ICOLUMN from T_ROOM_CONFIG;

SELECT EQUIPEMNTID,IGROUP,IROW,ICOLUMN from T_EQUIPMENT_LOCATION_MAP
 where IGROUP<=(select VVALUE from T_CONFIG where VKEY= 'group')
  and IROW<=(select VVALUE from T_CONFIG where VKEY= 'row') 
  and ICOLUMN<=(select VVALUE from T_CONFIG where VKEY= 'column')
  
  
  create table T_STUDENTINFO
(
	 STUDENTID 			varchar(32) PRIMARY KEY
	,NAME				VARCHAR(64)
	,SEX 				VARCHAR(8)
	,AGE 				INTEGER 
	,CLASS_NAME 		VARCHAR(128)
	,EMAIL				VARCHAR(128)
);

CREATE TABLE T_STUDENT_CHECK_INFO(
	 STUDENTID 			varchar(32) 
	,CHECK_TIME			VARCHAR(32)
	,SUBJECT_NAME 		VARCHAR(128)
	,STATUS 			VARCHAR(32)
	,PRIMARY KEY(STUDENTID,CHECK_TIME,SUBJECT_NAME)
);
INSERT into T_STUDENT_CHECK_INF(  STUDENTID ,CHECK_TIME ,CLASS_NAME ) VALUES('@STUDENTID','@CHECK_TIME','@CLASS_NAME');

CREATE TABLE T_QUESTION(
	 question_id 		varchar(32) PRIMARY KEY
	,caption		varchar(1024)
	,answer 		VARCHAR(8)  
	,question_index		varchar(8)
)
select question_id,caption,answer,question_index from T_QUESTION 
insert into T_QUESTION(question_id,caption,answer,question_index) values(@question_id,@caption,@answer,@question_index);
update T_QUESTION set caption = @caption,answer=@answer,question_index=@question_index where question_id = @question_id;
CREATE TABLE T_QUESTION(
	 QUESTION_NO 		int PRIMARY KEY
	,content 			TEXT
	,answer 			VARCHAR(8)  
)

SELECT  QUESTION_NO,content,answer 	FROM T_QUESTION
INSERT into T_QUESTION(content,answer) VALUES(@content,@answer);
UPDATE T_QUESTION set content= @content,answer=@answer where QUESTION_NO=@QUESTION_NO;
--°à¼¶³öÇÚÂÊ
CREATE TABLE T_CLASS_CHECK_INFO(
	 CHECK_TIME			VARCHAR(32)
	,CLASS_NAME 		VARCHAR(128)
	,PERCENTAGE			VARCHAR(16)
	,PRIMARY KEY(CHECK_TIME,CLASS_NAME)
);
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values(@CHECK_TIME,@CLASS_NAME,@PERCENTAGE);
SELECT CLASS_NAME as °à¼¶Ãû³Æ,CHECK_TIME as ¿¼ÇÚÊ±¼ä,PERCENTAGE as ³öÇÚÂÊ from T_CLASS_CHECK_INFO where CLASS_NAME = @CLASS_NAME and CHECK_TIME > @time_start and CHECK_TIME < @time_end


INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-13 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','80');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-14 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','100');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-15 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','70');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-16 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','90');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-17 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','40');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-18 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','70');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-19 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','80');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-20 09:15:47','ÐÅÏ¢Ñ§ÔºÒ»°à','90');

INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-13 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','80');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-14 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','100');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-15 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','70');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-16 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','90');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-17 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','40');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-18 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','70');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-19 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','80');
INSERT into T_CLASS_CHECK_INFO(CHECK_TIME,CLASS_NAME,PERCENTAGE) values('2011-12-20 09:15:47','ÐÅÏ¢Ñ§Ôº¶þ°à','90');




select STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL from T_STUDENTINFO

INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1001' ,'STUDENT1' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1002' ,'STUDENT2' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1003' ,'STUDENT3' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1004' ,'STUDENT4' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1005' ,'STUDENT5' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1006' ,'STUDENT6' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1007' ,'STUDENT7' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1008' ,'STUDENT8' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU1009' ,'STUDENT9' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§ÔºÒ»°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2001' ,'STUDENT10' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2002' ,'STUDENT11' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2003' ,'STUDENT12' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2004' ,'STUDENT13' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2005' ,'STUDENT14' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2006' ,'STUDENT15' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2007' ,'STUDENT16' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2008' ,'STUDENT17' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 
INSERT INTO T_STUDENTINFO(  STUDENTID,NAME,SEX,AGE,CLASS_NAME ,EMAIL) VALUES('STU2009' ,'STUDENT18' ,'ÄÐ' ,21 ,'ÐÅÏ¢Ñ§Ôº¶þ°à' ,'xx@example.com' ); 


001 011
002 012
003 021
004 022
005 031
006 032
007 111
008 112
009 121
010 122
011 131
012 132
013 211
014 212
015 221
016 222
017 231
018 232
