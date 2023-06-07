-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.45-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema mridge
--

CREATE DATABASE IF NOT EXISTS mridge;
USE mridge;

--
-- Definition of table `acco`
--

DROP TABLE IF EXISTS `acco`;
CREATE TABLE `acco` (
  `accoid` varchar(12) NOT NULL,
  `subid` varchar(12) NOT NULL,
  `checkin` date NOT NULL,
  `checkout` date NOT NULL,
  `regpax` int(10) unsigned NOT NULL,
  `senior` int(10) unsigned NOT NULL,
  `child` int(10) unsigned NOT NULL,
  `serviceid` int(10) unsigned NOT NULL,
  `void` char(1) NOT NULL,
  PRIMARY KEY  (`accoid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `acco`
--

/*!40000 ALTER TABLE `acco` DISABLE KEYS */;
/*!40000 ALTER TABLE `acco` ENABLE KEYS */;


--
-- Definition of table `billing`
--

DROP TABLE IF EXISTS `billing`;
CREATE TABLE `billing` (
  `billingId` varchar(12) NOT NULL,
  `userid` varchar(15) NOT NULL,
  `guestid` varchar(15) NOT NULL,
  `flag` varchar(6) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY  (`billingId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `billing`
--

/*!40000 ALTER TABLE `billing` DISABLE KEYS */;
/*!40000 ALTER TABLE `billing` ENABLE KEYS */;


--
-- Definition of table `guest`
--

DROP TABLE IF EXISTS `guest`;
CREATE TABLE `guest` (
  `guestid` varchar(10) NOT NULL,
  `lname` varchar(20) NOT NULL,
  `fname` varchar(30) NOT NULL,
  `mname` varchar(20) NOT NULL,
  `add` varchar(100) NOT NULL,
  `phonenum` varchar(15) NOT NULL,
  `e-mailadd` varchar(45) NOT NULL,
  `company` varchar(100) NOT NULL,
  `officeadd` varchar(100) NOT NULL,
  `telnumber` varchar(15) NOT NULL,
  `dreg` date NOT NULL,
  PRIMARY KEY  (`guestid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `guest`
--

/*!40000 ALTER TABLE `guest` DISABLE KEYS */;
INSERT INTO `guest` (`guestid`,`lname`,`fname`,`mname`,`add`,`phonenum`,`e-mailadd`,`company`,`officeadd`,`telnumber`,`dreg`) VALUES 
 ('2012-0001','','Walk - In','','','','','','','','2012-08-16'),
 ('2012-0002','Ganiga','Cicelle','Tioxon','Suring Pangobilian Brooke\'s Point Palawan','21555524755744','cel_joy07@yahoo.com','','','','2012-07-15'),
 ('2012-0003','Baliling','Elah','Sangaw','Sorigao City','456554655494','elah_clear06@yahoo.com','','','','2012-07-15'),
 ('2012-0004','Salon','Mae','Milo','Boligay Brooke\'s Point Palawan\n','55149465449','mea_me09@yahoo.com','','','','2012-07-15'),
 ('2012-0005','Palapa','Rhea','Pilapil','Balacan Brooke\'s Point Palawan','58612454641554','re_rhea03@yahoo.com','','','','2012-07-15'),
 ('2012-0006','Manalo','Mary Grace','Villon','Poblacion District 2 Brooke\'s Point, Palawan','09172576783','marygracemanalo75@yahoo.com','','','','2012-07-15'),
 ('2012-0007','Anas','Kieyah Zannica','Manalo','Suring Pangobilia Brooke\'s Point, Palawan','0905487654','Keiyah_Anas12@yahoo.com','','','','2012-07-15'),
 ('2012-0008','Ganiga','Michelle','Tioxon','Suring Pangobilian Brooke\'s Point Palawan','09095491506','Mitch_maih2010@yahoo.com','','','','2012-07-15');
INSERT INTO `guest` (`guestid`,`lname`,`fname`,`mname`,`add`,`phonenum`,`e-mailadd`,`company`,`officeadd`,`telnumber`,`dreg`) VALUES 
 ('2012-0009','Ausria','Helen Grace','Socrates','Buligay Brooke\'s Point, Palawan','09156897215','Helen_Ausrria12@yahoo.com','','','','2012-07-15'),
 ('2012-0010','lfksjdflk','lfsdlkjf','lksdjflk','lfsddkjflk','flsdkjflk','sldfjlk','fsldjflk','','','2012-08-28'),
 ('2012-0011','kjdhk','jashdkj','kjdh','jdhk','jh','','','','','2012-08-28'),
 ('2012-0012','Tamaño','Liza','Saba','Tiniguiban','','','','','','2012-08-28'),
 ('2012-0013','Floyd','Flora','Riomon','Sagrado','19910214','sample@ymail.com','Forbid Corp.','Sagrado','19880820','2012-09-10');
/*!40000 ALTER TABLE `guest` ENABLE KEYS */;


--
-- Definition of table `payment`
--

DROP TABLE IF EXISTS `payment`;
CREATE TABLE `payment` (
  `payid` int(10) unsigned NOT NULL auto_increment,
  `billingid` int(10) unsigned NOT NULL,
  `totalamount` double NOT NULL,
  `cash` double NOT NULL,
  `balance` double NOT NULL,
  `change` double NOT NULL,
  `date` datetime NOT NULL,
  `VAT` double NOT NULL,
  `VATable` double NOT NULL,
  PRIMARY KEY  (`payid`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `payment`
--

/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;


--
-- Definition of table `reserve`
--

DROP TABLE IF EXISTS `reserve`;
CREATE TABLE `reserve` (
  `reserveid` int(10) unsigned NOT NULL auto_increment,
  `userid` varchar(45) NOT NULL,
  `serviceid` varchar(45) NOT NULL,
  `guestid` varchar(45) NOT NULL,
  `checkin` datetime NOT NULL,
  `checkout` datetime NOT NULL,
  `status` varchar(8) NOT NULL,
  `roomtype` varchar(12) NOT NULL,
  PRIMARY KEY  (`reserveid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `reserve`
--

/*!40000 ALTER TABLE `reserve` DISABLE KEYS */;
/*!40000 ALTER TABLE `reserve` ENABLE KEYS */;


--
-- Definition of table `service`
--

DROP TABLE IF EXISTS `service`;
CREATE TABLE `service` (
  `serviceid` int(10) unsigned NOT NULL auto_increment,
  `name` varchar(20) NOT NULL,
  `type` varchar(20) NOT NULL,
  `class` varchar(20) NOT NULL,
  `price` double NOT NULL,
  `max` int(10) unsigned NOT NULL,
  `category` varchar(13) NOT NULL,
  `limit` int(10) unsigned NOT NULL,
  `kind` varchar(10) NOT NULL,
  PRIMARY KEY  USING BTREE (`serviceid`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `service`
--

/*!40000 ALTER TABLE `service` DISABLE KEYS */;
INSERT INTO `service` (`serviceid`,`name`,`type`,`class`,`price`,`max`,`category`,`limit`,`kind`) VALUES 
 (1,'D1','De Luxe','Room',1350,4,'Accommodation',2,'Regular'),
 (2,'D2','De Luxe','Room',1350,4,'Accommodation',2,'Regular'),
 (3,'S1','Standard','Room',1450,4,'Accommodation',2,'Regular'),
 (4,'S2','Standard','Room',1450,4,'Accommodation',2,'Regular'),
 (5,'F1','Family','Room',1650,6,'Accommodation',4,'Regular'),
 (6,'F2','Family','Room',1650,6,'Accommodation',4,'Regular'),
 (7,'Hall','Open Hall','Room',5000,100,'Convention',0,'Regular'),
 (8,'VIP','Air Conditioned','Room',3000,50,'Convention',0,'Regular'),
 (9,'Additional Pax','Add Pax','Room',300,0,'Accommodation',0,'Additional'),
 (10,'Exceed Time','Hall','Room',500,0,'Convention',0,'Additional'),
 (11,'Chicken Curry','Curry','Resto',200,0,'',0,''),
 (12,'Pork Adobo','Pork','Resto',200,0,'',0,''),
 (13,'Bourbon Chicken','Curry','Resto',250,0,'',0,''),
 (14,'Hawaiian Pizza','Pizza','Resto',300,0,'',0,''),
 (15,'D5','De Luxe','Room',1350,4,'Accommodation',2,'Regular'),
 (16,'Beef Steak','Beef','Resto',350,0,'',0,'Regular');
INSERT INTO `service` (`serviceid`,`name`,`type`,`class`,`price`,`max`,`category`,`limit`,`kind`) VALUES 
 (18,'D3','De Luxe','Room',1350,4,'Accommodation',2,'Regular'),
 (19,'Additional Hour','Extend','Room',200,0,'Accommodation',0,'Additional'),
 (20,'Dinuguan','Main Course','Resto',200,0,'',0,'Regular'),
 (21,'Thin Crust','Pizza','Resto',400,0,'',0,'Regular');
/*!40000 ALTER TABLE `service` ENABLE KEYS */;


--
-- Definition of table `sub`
--

DROP TABLE IF EXISTS `sub`;
CREATE TABLE `sub` (
  `subid` varchar(12) NOT NULL,
  `billingid` varchar(12) NOT NULL,
  `serviceid` int(10) unsigned NOT NULL,
  `qty` double NOT NULL,
  `price` double NOT NULL,
  `amount` double NOT NULL,
  `discount` double NOT NULL,
  `discountedprice` double NOT NULL,
  `vat` double NOT NULL,
  `vatable` double NOT NULL,
  `void` char(1) NOT NULL,
  `payflag` char(1) NOT NULL,
  PRIMARY KEY  USING BTREE (`subid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sub`
--

/*!40000 ALTER TABLE `sub` DISABLE KEYS */;
/*!40000 ALTER TABLE `sub` ENABLE KEYS */;


--
-- Definition of table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `LastName` varchar(20) NOT NULL,
  `FirstName` varchar(20) NOT NULL,
  `MiddleName` varchar(20) NOT NULL,
  `ContactNo` varchar(15) NOT NULL,
  `CivilStat` varchar(10) NOT NULL,
  `Gender` varchar(6) NOT NULL,
  `DateofBirth` date NOT NULL,
  `Address` varchar(80) NOT NULL,
  `UserName` varchar(20) NOT NULL,
  `Password` varchar(20) NOT NULL,
  `userid` int(10) unsigned NOT NULL auto_increment,
  `secques` varchar(80) NOT NULL,
  `secans` varchar(20) NOT NULL,
  `utype` varchar(10) NOT NULL,
  `ustatus` varchar(8) NOT NULL,
  PRIMARY KEY  (`userid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`LastName`,`FirstName`,`MiddleName`,`ContactNo`,`CivilStat`,`Gender`,`DateofBirth`,`Address`,`UserName`,`Password`,`userid`,`secques`,`secans`,`utype`,`ustatus`) VALUES 
 ('Joya','Dionisio','Espolon','09352673133','Single','Male','1991-07-24','Balacan Brooke\'s Point Palawan','CanoiJoya','YonexLining',1,'Who is your favorate anime character?','Son Guko','Admin','Active'),
 ('Ganiga','Mechelle','Tioxon','09095491506','Single','Female','1992-06-07','Brooke\'s Point Palawan','mitch_','chelle',2,'Who is your childhood hero?','Rizal','Front Desk','Active');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;


--
-- Definition of table `vatdiscount`
--

DROP TABLE IF EXISTS `vatdiscount`;
CREATE TABLE `vatdiscount` (
  `id` int(10) unsigned NOT NULL auto_increment,
  `name` varchar(20) NOT NULL,
  `value` double NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `vatdiscount`
--

/*!40000 ALTER TABLE `vatdiscount` DISABLE KEYS */;
INSERT INTO `vatdiscount` (`id`,`name`,`value`) VALUES 
 (1,'Vat',12),
 (2,'Senior Citizen',20);
/*!40000 ALTER TABLE `vatdiscount` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
