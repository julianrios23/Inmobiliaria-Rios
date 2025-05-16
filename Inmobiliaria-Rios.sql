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
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `idclientes` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) DEFAULT NULL,
  `apellido` varchar(45) DEFAULT NULL,
  `dni` int DEFAULT NULL,
  `mail` varchar(45) DEFAULT NULL,
  `telefono` bigint DEFAULT NULL,
  `domicilio` varchar(45) DEFAULT NULL,
  `localidad` varchar(45) DEFAULT NULL,
  `provincia` varchar(45) DEFAULT NULL,
  `estado` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`idclientes`),
  UNIQUE KEY `idinquilinos_UNIQUE` (`idclientes`),
  UNIQUE KEY `dni_UNIQUE` (`dni`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (4,'María','Gómez',27894561,'maria.gomez@email.com',2664012345,'Calle Falsa 125','Villa Mercedes','San Luis',1),(5,'Carlos','López',31234567,'carlos.lopez@email.com',2664023456,'Las Heras 457','Merlo','San Luis',1),(6,'Ana','Fernández',29587412,'ana.fernandez@email.com',2664035679,'Av. Libertador 789','San Luis','San Luis',1),(7,'Jorge','Pérez',30985647,'jorge.perez@email.com',2664046789,'San Martín 987','Villa Mercedes','San Luis',1),(8,'Sofía','Rodríguez',31569874,'sofia.rodriguez@email.com',2664057890,'9 de Julio 321','Merlo','San Luis',1),(9,'Lucas','García',28756412,'lucas.garcia@email.com',2664068901,'España 654','La Toma','San Luis',1),(10,'Natalia','Martínez',30147895,'natalia.martinez@email.com',2664079012,'Mitre 852','Tilisarao','San Luis',1),(11,'Martín','Sánchez',32781456,'martin.sanchez@email.com',2664080123,'Belgrano 963','Juana Koslay','San Luis',1),(12,'Julieta','Ramírez',29874563,'julieta.ramirez@email.com',2664091231,'Rivadavia 753','Concarán','San Luis',1),(13,'Pablo','Méndez',31025478,'pablo.mendez@email.com',2664102345,'Pringles 562','San Luis','San Luis',1),(14,'Morena','Rios Escobar',50200100,'more@mail.com',3777300600,'25 de Mayo 1636','La Toma','San Luis',1),(15,'Cecilia','Zone',28333666,'laura@mail.com',266321456,'9 de Julio 509','Villa Mercedes','San Luis',1),(16,'Pablo','Poder',33000222,'pablo@pablo.com',264336622,'Av Los Arboles 1516','San Luis','San Luis',1),(17,'Pdro','Sa',2211133,'pedrito@pe.com',3214563,'Barcelona 2526','Roque Perez','Santa Fe',1);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contratos`
--

DROP TABLE IF EXISTS `contratos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contratos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `idinmuebles` int NOT NULL,
  `idclientes` int NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFin` date NOT NULL,
  `FechaFinAnticipada` date DEFAULT NULL,
  `MontoMensual` decimal(18,2) NOT NULL,
  `Multa` decimal(18,2) DEFAULT NULL,
  `Estado` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `UsuarioCreacionId` int NOT NULL,
  `FechaCreacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UsuarioModificacionId` int DEFAULT NULL,
  `FechaModificacion` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `fk_inmuebles` (`idinmuebles`),
  KEY `fk_clientes` (`idclientes`),
  KEY `fk_usuario_creacion` (`UsuarioCreacionId`),
  KEY `fk_usuario_modificacion` (`UsuarioModificacionId`),
  CONSTRAINT `fk_clientes` FOREIGN KEY (`idclientes`) REFERENCES `clientes` (`idclientes`) ON UPDATE CASCADE,
  CONSTRAINT `fk_inmuebles` FOREIGN KEY (`idinmuebles`) REFERENCES `inmuebles` (`idinmuebles`) ON UPDATE CASCADE,
  CONSTRAINT `fk_usuario_creacion` FOREIGN KEY (`UsuarioCreacionId`) REFERENCES `usuarios` (`idusuarios`) ON UPDATE CASCADE,
  CONSTRAINT `fk_usuario_modificacion` FOREIGN KEY (`UsuarioModificacionId`) REFERENCES `usuarios` (`idusuarios`) ON UPDATE CASCADE,
  CONSTRAINT `chk_estado` CHECK ((`Estado` in (_utf8mb4'Vigente',_utf8mb4'Finalizado',_utf8mb4'Cancelado')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contratos`
--

LOCK TABLES `contratos` WRITE;
/*!40000 ALTER TABLE `contratos` DISABLE KEYS */;
/*!40000 ALTER TABLE `contratos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagenes`
--

DROP TABLE IF EXISTS `imagenes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagenes` (
  `idimagen` int NOT NULL AUTO_INCREMENT,
  `idinmueble` int NOT NULL,
  `ruta_imagen` varchar(255) NOT NULL,
  PRIMARY KEY (`idimagen`),
  KEY `idinmueble` (`idinmueble`),
  CONSTRAINT `imagenes_ibfk_1` FOREIGN KEY (`idinmueble`) REFERENCES `inmuebles` (`idinmuebles`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagenes`
--

LOCK TABLES `imagenes` WRITE;
/*!40000 ALTER TABLE `imagenes` DISABLE KEYS */;
INSERT INTO `imagenes` VALUES (1,13,'imagenes\\30556c27-9fff-4a67-a88b-3ef60df3f85e.jpg'),(2,13,'imagenes\\68c5f015-3c5c-484d-9df1-03478f4895c6.jpg'),(8,15,'imagenes\\49206e20-5206-4d5a-9615-a6e8ab3e6e8d.jpg'),(9,15,'imagenes\\4f36369b-97be-4db9-a748-921a91bb238d.jpg'),(11,14,'imagenes\\12fee15a-4be5-4e3d-b63c-cf01ff1ea808.jpg'),(12,14,'imagenes\\bd870f02-2046-4fba-97ec-528c1a8606ac.jpg'),(13,14,'imagenes\\aa704129-6b3c-4369-a0f2-c2075486c387.jpg'),(17,16,'imagenes\\3863d24d-89b3-49f7-9237-d460f921ac79.jpg'),(18,17,'imagenes\\e23595ca-04bf-4425-a858-75c79780d2dc.jpg');
/*!40000 ALTER TABLE `imagenes` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `inmuebles`
--

LOCK TABLES `inmuebles` WRITE;
/*!40000 ALTER TABLE `inmuebles` DISABLE KEYS */;
INSERT INTO `inmuebles` VALUES (13,12,'Las Piedras 963','La Punta','San Luis','local',1,1,'no',NULL,'no','no','no',1,'si','no','alquiler',470000,'Local luminoso de 120 m2 ideal para local ropa',1),(14,9,'Las Moras 1101','Villa Mercedes','San Luis','casa',4,2,'si',1,'si','no','no',1,'si','si','venta',50000000,'Vivienda de 10 x 25. 162 m2 de construccion',1),(15,13,'Carlos Gardel 901','Juana Koslay','San Luis','local',2,1,'no',NULL,'no','no','no',1,'si','no','alquiler',635000,'Esquina centrica en pleno corazon comercial de la ciudad',1),(16,2,'Av Velez Sarfield 4251','Rosario','Santa Fe','departamento',3,1,'si',1,'no','no','si',1,'si','no','alquiler',25000,NULL,1),(17,1,'Av Sarmiento 955','La Punta','San Luis','local',1,1,'no',NULL,'no','no','no',1,'no','no','alquiler',1000000,'aaaaaa',1);
/*!40000 ALTER TABLE `inmuebles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pagos`
--

DROP TABLE IF EXISTS `pagos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pagos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ContratoId` int NOT NULL,
  `NumeroPago` int NOT NULL,
  `FechaPago` date NOT NULL,
  `Importe` decimal(18,2) NOT NULL,
  `Concepto` varchar(100) NOT NULL,
  `Estado` varchar(20) NOT NULL,
  `UsuarioCreacionId` int NOT NULL,
  `FechaCreacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UsuarioAnulacionId` int DEFAULT NULL,
  `FechaAnulacion` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `ContratoId` (`ContratoId`),
  KEY `UsuarioCreacionId` (`UsuarioCreacionId`),
  KEY `UsuarioAnulacionId` (`UsuarioAnulacionId`),
  CONSTRAINT `pagos_ibfk_1` FOREIGN KEY (`ContratoId`) REFERENCES `contratos` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `pagos_ibfk_2` FOREIGN KEY (`UsuarioCreacionId`) REFERENCES `usuarios` (`idusuarios`),
  CONSTRAINT `pagos_ibfk_3` FOREIGN KEY (`UsuarioAnulacionId`) REFERENCES `usuarios` (`idusuarios`),
  CONSTRAINT `chk_estado_pago` CHECK ((`Estado` in (_utf8mb4'Pagado',_utf8mb4'Anulado')))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pagos`
--

LOCK TABLES `pagos` WRITE;
/*!40000 ALTER TABLE `pagos` DISABLE KEYS */;
/*!40000 ALTER TABLE `pagos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `propietarios`
--

DROP TABLE IF EXISTS `propietarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `propietarios` (
  `idpropietario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) DEFAULT NULL,
  `apellido` varchar(45) DEFAULT NULL,
  `dni` int DEFAULT NULL,
  `cuit` varchar(45) DEFAULT NULL,
  `telefono` varchar(45) DEFAULT NULL,
  `mail` varchar(45) DEFAULT NULL,
  `domicilio` varchar(100) DEFAULT NULL,
  `localidad` varchar(45) DEFAULT NULL,
  `provincia` varchar(45) DEFAULT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT '1',
  UNIQUE KEY `idpropietario_UNIQUE` (`idpropietario`),
  UNIQUE KEY `dni_UNIQUE` (`dni`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `propietarios`
--

LOCK TABLES `propietarios` WRITE;
/*!40000 ALTER TABLE `propietarios` DISABLE KEYS */;
INSERT INTO `propietarios` VALUES (1,'Juan','Pérez',30123456,'20-30123456-5','2664001235','juan.perez@email.com','Av. Siempre Viva 742','San Luis','San Luis',1),(2,'Liliana','Contreras',25000100,'20-25000100-7','1164009632','lili@email.com','Sarmiento 513','CABA','Buenos Aires',1),(3,'Carlos','Martinez',20147852,'30-20147852-0','34169693214','carlos@mail.com','Las Toscas 489','Villa Gdor Galvez','Santa Fe',1),(6,'Moni','Argento',24147852,'20-24147852-7','266547896','moni@mail.com','Peron 1478','San Luis','San Luis',1),(8,'','',0,'20-23123456-9','35145632147','sergio@mail.com','Brown 1548','Cordoba','Cordoba',1),(9,'Aldana','Rios Escobar',40908898,'25-40908898-4','34125637892','aldana@mail.com','San Martin 508','Villa Mercedes','San Luis',1),(10,'Julieta','Marquez',15456369,'20-15456369-8','264415267','marquez@mail.com','Av Las Camelias 1896','San Luis','San Luis',1),(11,'Diana','Escobar',27018746,'27-27018746-9','3777400326','dianita@mail.com','Las Heras 1255','Resistencia','Chaco',0),(12,'Juan Carlos','Herrera',22789456,'30-22789456-0','1145631479','carlito@mailcito.com','Juana Manso 366','Lujan','Buenos Aires',1),(13,'Pedro','Alfonso',5963852,'21-05963852-9','33333333334','pedrito@email.com','Sarmiento 801','Juana Koslay','San Luis',1);
/*!40000 ALTER TABLE `propietarios` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `idusuarios` int NOT NULL,
  `nombre` varchar(45) DEFAULT NULL,
  `apellido` varchar(45) DEFAULT NULL,
  `telefono` int DEFAULT NULL,
  `mail` varchar(45) DEFAULT NULL,
  `rol` enum('admin','empleado') DEFAULT NULL,
  `contraseña` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`idusuarios`),
  UNIQUE KEY `idusuarios_UNIQUE` (`idusuarios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Julian','Rios',123456789,'admin@gmail.com','admin','admin123'),(2,'Pepe','Argento',987654321,'pepe@gmail.com','empleado','pepe123');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-16 19:48:31
