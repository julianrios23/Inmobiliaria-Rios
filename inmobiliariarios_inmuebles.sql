-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: localhost    Database: inmobiliariarios
-- ------------------------------------------------------
-- Server version	8.0.33

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `inmuebles`
--

DROP TABLE IF EXISTS `inmuebles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `inmuebles` (
  `idinmuebles` int NOT NULL AUTO_INCREMENT,
  `idpropietario` int DEFAULT NULL,
  `direccion` varchar(45) DEFAULT NULL,
  `localidad` varchar(45) DEFAULT NULL,
  `provincia` varchar(45) DEFAULT NULL,
  `tipo` enum('casa','departamento','local','lote','cabana') DEFAULT NULL,
  `ambientes` int DEFAULT NULL,
  `banos` int DEFAULT NULL,
  `garage` enum('si','no') DEFAULT NULL,
  `capGarage` int DEFAULT NULL,
  `patio` enum('si','no') DEFAULT NULL,
  `piscina` enum('si','no') DEFAULT NULL,
  `terraza` enum('si','no') DEFAULT NULL,
  `plantas` int DEFAULT NULL,
  `cocina` enum('si','no') DEFAULT NULL,
  `parrilla` enum('si','no') DEFAULT NULL,
  `opcion` enum('alquiler','venta') DEFAULT NULL,
  `precio` int DEFAULT NULL,
  `observaciones` varchar(150) DEFAULT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`idinmuebles`),
  KEY `idpropietario_idx` (`idpropietario`),
  CONSTRAINT `idpropietario` FOREIGN KEY (`idpropietario`) REFERENCES `propietarios` (`idpropietario`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inmuebles`
--

LOCK TABLES `inmuebles` WRITE;
/*!40000 ALTER TABLE `inmuebles` DISABLE KEYS */;
INSERT INTO `inmuebles` VALUES (2,1,'Av San Martin 1586','Villa Mercedes','San Luis','departamento',3,1,'si',NULL,'no','no','si',1,'si','no','alquiler',450000,'A metros de plaza principal',0),(5,2,'Av Sarmiento 955','San Luis','San Luis','local',1,1,'si',2,'no','no','no',1,'no','no','alquiler',650000,'Local centrico con amplia vidriera y cortina metalica',1),(6,2,'Carlos Gardel 987','Rio Cuarto','Cordoba','local',1,1,'no',NULL,'no','no','no',1,'no','no','alquiler',400000,'Amplio local de 96 m2 con vidriera y cortina metalica',1),(7,1,'Belgrano 1944','La Punta','San Luis','casa',4,2,'si',1,'si','no','no',1,'si','si','venta',80000000,'Casa restaurada con mejoras funcionales.',1),(8,1,'San Lorenzo 301','Merlo','San Luis','local',2,1,'no',NULL,'no','no','no',1,'si','no','alquiler',552000,'Local en esquina tradicional. Ideal Artesanias',1),(9,9,'Las Moras 1101','La Punta','San Luis','casa',3,1,'no',NULL,'si','no','no',1,'si','si','alquiler',490000,'linda',1),(10,11,'Francia 5600','Juana Koslay','San Luis','departamento',2,1,'no',NULL,'no','no','si',1,'si','no','alquiler',290000,'Iluminado',1),(11,6,'Triunvurato 1955','La Punta','San Luis','departamento',3,1,'si',1,'no','no','si',1,'si','no','alquiler',430000,'muy lindo...',1),(12,10,'Coronel Pringles 1620','San Luis','San Luis','local',2,1,'no',NULL,'no','no','no',2,'si','no','alquiler',812000,'Local con vidriera amplia con deposito en 1er piso. 200 m2.....',1);
/*!40000 ALTER TABLE `inmuebles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-03 23:41:34
