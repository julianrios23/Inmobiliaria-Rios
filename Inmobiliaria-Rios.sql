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
  `estado` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idclientes`),
  UNIQUE KEY `idinquilinos_UNIQUE` (`idclientes`),
  UNIQUE KEY `dni_UNIQUE` (`dni`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (4,'María','Gómez',27894561,'maria.gomez@email.com',2664012345,'Calle Falsa 125','Villa Mercedes','San Luis',1),(5,'Carlos','López',31234567,'carlos.lopez@email.com',2664023456,'Las Heras 457','Merlo','San Luis',1),(6,'Ana','Fernández',29587412,'ana.fernandez@email.com',2664035678,'Av. Libertador 789','San Luis','San Luis',1),(7,'Jorge','Pérez',30985647,'jorge.perez@email.com',2664046789,'San Martín 987','Villa Mercedes','San Luis',1),(8,'Sofía','Rodríguez',31569874,'sofia.rodriguez@email.com',2664057890,'9 de Julio 321','Merlo','San Luis',1),(9,'Lucas','García',28756412,'lucas.garcia@email.com',2664068901,'España 654','La Toma','San Luis',1),(10,'Natalia','Martínez',30147895,'natalia.martinez@email.com',2664079012,'Mitre 852','Tilisarao','San Luis',1),(11,'Martín','Sánchez',32781456,'martin.sanchez@email.com',2664080123,'Belgrano 963','Juana Koslay','San Luis',1),(12,'Julieta','Ramírez',29874563,'julieta.ramirez@email.com',2664091231,'Rivadavia 753','Concarán','San Luis',1),(13,'Pablo','Méndez',31025478,'pablo.mendez@email.com',2664102345,'Pringles 562','San Luis','San Luis',1),(14,'Morena','Rios Escobar',50200100,'more@mail.com',3777300600,'25 de Mayo 1636','La Toma','San Luis',0),(15,'Cecilia','Zone',28333666,'laura@mail.com',266321456,'9 de Julio 509','Villa Mercedes','San Luis',0);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contrato`
--

DROP TABLE IF EXISTS `contrato`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contrato` (
  `id` int NOT NULL AUTO_INCREMENT,
  `inmueble_id` int NOT NULL,
  `inquilino_id` int NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `monto_mensual` decimal(10,2) NOT NULL,
  `fecha_cancelacion_anticipada` date DEFAULT NULL,
  `multa` decimal(10,2) DEFAULT NULL,
  `creado_por_usuario_id` int NOT NULL,
  `cancelado_por_usuario_id` int DEFAULT NULL,
  `fecha_creacion` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `inmueble_id` (`inmueble_id`),
  KEY `inquilino_id` (`inquilino_id`),
  KEY `creado_por_usuario_id` (`creado_por_usuario_id`),
  KEY `cancelado_por_usuario_id` (`cancelado_por_usuario_id`),
  CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`inmueble_id`) REFERENCES `inmuebles` (`idinmuebles`),
  CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`inquilino_id`) REFERENCES `clientes` (`idclientes`),
  CONSTRAINT `contrato_ibfk_3` FOREIGN KEY (`creado_por_usuario_id`) REFERENCES `usuarios` (`idusuarios`),
  CONSTRAINT `contrato_ibfk_4` FOREIGN KEY (`cancelado_por_usuario_id`) REFERENCES `usuarios` (`idusuarios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contrato`
--

LOCK TABLES `contrato` WRITE;
/*!40000 ALTER TABLE `contrato` DISABLE KEYS */;
/*!40000 ALTER TABLE `contrato` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagenes_inmuebles`
--

DROP TABLE IF EXISTS `imagenes_inmuebles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagenes_inmuebles` (
  `idimagen` int NOT NULL AUTO_INCREMENT,
  `idinmueble` int NOT NULL,
  `ruta_imagen` varchar(255) NOT NULL,
  PRIMARY KEY (`idimagen`),
  KEY `idinmueble` (`idinmueble`),
  CONSTRAINT `imagenes_inmuebles_ibfk_1` FOREIGN KEY (`idinmueble`) REFERENCES `inmuebles` (`idinmuebles`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagenes_inmuebles`
--

LOCK TABLES `imagenes_inmuebles` WRITE;
/*!40000 ALTER TABLE `imagenes_inmuebles` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagenes_inmuebles` ENABLE KEYS */;
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

--
-- Table structure for table `pago`
--

DROP TABLE IF EXISTS `pago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pago` (
  `id` int NOT NULL AUTO_INCREMENT,
  `contrato_id` int NOT NULL,
  `numero_pago` int NOT NULL,
  `fecha_pago` date NOT NULL,
  `detalle` varchar(255) DEFAULT NULL,
  `importe` decimal(10,2) NOT NULL,
  `anulado` tinyint(1) NOT NULL DEFAULT '0',
  `creado_por_usuario_id` int NOT NULL,
  `anulado_por_usuario_id` int DEFAULT NULL,
  `fecha_creacion` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `contrato_id` (`contrato_id`),
  KEY `creado_por_usuario_id` (`creado_por_usuario_id`),
  KEY `anulado_por_usuario_id` (`anulado_por_usuario_id`),
  CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`contrato_id`) REFERENCES `contrato` (`id`),
  CONSTRAINT `pago_ibfk_2` FOREIGN KEY (`creado_por_usuario_id`) REFERENCES `usuarios` (`idusuarios`),
  CONSTRAINT `pago_ibfk_3` FOREIGN KEY (`anulado_por_usuario_id`) REFERENCES `usuarios` (`idusuarios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pago`
--

LOCK TABLES `pago` WRITE;
/*!40000 ALTER TABLE `pago` DISABLE KEYS */;
/*!40000 ALTER TABLE `pago` ENABLE KEYS */;
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
INSERT INTO `propietarios` VALUES (1,'Juan','Pérez',30123456,'20-30123456-5','2664001235','juan.perez@email.com','Av. Siempre Viva 742','San Luis','San Luis',1),(2,'Liliana','Contreras',25000100,'20-25000100-7','1164009632','lili@email.com','Sarmiento 513','CABA','Buenos Aires',1),(3,'Carlos','Martinez',20147852,'30-20147852-0','34169693214','carlos@mail.com','Las Toscas 489','Villa Gdor Galvez','Santa Fe',1),(6,'Moni','Argento',24147852,'20-24147852-7','266547896','moni@mail.com','Peron 1478','San Luis','San Luis',1),(8,'Sergio','Martinez',23123456,'20-23123456-9','35145632145','sergio@mail.com','Brown 1548','Cordoba','Cordoba',1),(9,'Aldana','Rios Escobar',40908898,'25-40908898-4','34125637892','aldana@mail.com','San Martin 508','Villa Mercedes','San Luis',1),(10,'Julieta','Marquez',15456369,'20-15456369-8','264415267','marquez@mail.com','Av Las Camelias 1896','San Luis','San Luis',1),(11,'Diana','Escobar',27018746,'27-27018746-9','3777400326','dianita@mail.com','Las Heras 1255','Resistencia','Chaco',0),(12,'Juan Carlos','Herrera',22789456,'30-22789456-0','1145631479','carlito@mailcito.com','Juana Manso 366','Lujan','Buenos Aires',1),(13,'Pedro','Alfonso',5963852,'21-05963852-9','33333333334','pedrito@email.com','Sarmiento 801','Juana Koslay','San Luis',1);
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
  PRIMARY KEY (`idusuarios`),
  UNIQUE KEY `idusuarios_UNIQUE` (`idusuarios`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
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

-- Dump completed on 2025-04-10 12:07:18
