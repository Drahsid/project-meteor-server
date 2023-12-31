-- MySQL dump 10.13  Distrib 5.7.10, for Win64 (x86_64)
--
-- Host: localhost    Database: ffxiv_database
-- ------------------------------------------------------
-- Server version	5.7.10-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `characters_class_levels`
--

DROP TABLE IF EXISTS `characters_class_levels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `characters_class_levels` (
  `characterId` int(10) unsigned NOT NULL,
  `pug` smallint(6) DEFAULT '0',
  `gla` smallint(6) DEFAULT '0',
  `mrd` smallint(6) DEFAULT '0',
  `arc` smallint(6) DEFAULT '0',
  `lnc` smallint(6) DEFAULT '0',
  `thm` smallint(6) DEFAULT '0',
  `cnj` smallint(6) DEFAULT '0',
  `crp` smallint(6) DEFAULT '0',
  `bsm` smallint(6) DEFAULT '0',
  `arm` smallint(6) DEFAULT '0',
  `gsm` smallint(6) DEFAULT '0',
  `ltw` smallint(6) DEFAULT '0',
  `wvr` smallint(6) DEFAULT '0',
  `alc` smallint(6) DEFAULT '0',
  `cul` smallint(6) DEFAULT '0',
  `min` smallint(6) DEFAULT '0',
  `btn` smallint(6) DEFAULT '0',
  `fsh` smallint(6) DEFAULT '0',
  PRIMARY KEY (`characterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `characters_class_levels`
--

LOCK TABLES `characters_class_levels` WRITE;
/*!40000 ALTER TABLE `characters_class_levels` DISABLE KEYS */;

/*!40000 ALTER TABLE `characters_class_levels` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-06-07 22:54:45
