-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Apr 24, 2017 at 06:16 PM
-- Server version: 10.1.16-MariaDB
-- PHP Version: 5.6.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dienynas`
--

-- --------------------------------------------------------

--
-- Table structure for table `dalykai`
--

CREATE TABLE `dalykai` (
  `id` int(11) NOT NULL,
  `dalykas` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `dalykai`
--

INSERT INTO `dalykai` (`id`, `dalykas`) VALUES
(1, 'Matematika'),
(2, 'Biologija'),
(3, 'Geografija'),
(4, 'Fizika'),
(5, 'Chemija'),
(6, 'Istorija'),
(7, 'Bendroji kūno kultūra'),
(8, 'Dailė'),
(9, 'Informacinės technologijos'),
(10, 'Lietuvių kalba (gimtoji)'),
(11, 'Muzika'),
(12, 'Pilietiškumo pagrindai'),
(13, 'Technologijos'),
(14, 'Užsienio kalba (anglų)'),
(15, 'Užsienio kalba (rusų)'),
(16, 'Dorinis ugdymas (tikyba)');

-- --------------------------------------------------------

--
-- Table structure for table `klases`
--

CREATE TABLE `klases` (
  `id` int(11) NOT NULL,
  `klase` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `klases`
--

INSERT INTO `klases` (`id`, `klase`) VALUES
(1, '1A'),
(2, '1B'),
(3, '1C'),
(4, '2A'),
(5, '2B'),
(6, '3A'),
(7, '3C'),
(8, '3B'),
(9, '2C'),
(10, '4B'),
(11, '4C'),
(12, '4A');

-- --------------------------------------------------------

--
-- Table structure for table `kodai`
--

CREATE TABLE `kodai` (
  `id` int(11) NOT NULL,
  `kodas` char(10) NOT NULL,
  `zmogus` int(11) NOT NULL,
  `tipas` tinyint(4) NOT NULL DEFAULT '0',
  `klase` int(11) NOT NULL DEFAULT '0',
  `parent` int(11) NOT NULL DEFAULT '0',
  `dalykas` tinyint(4) NOT NULL DEFAULT '0',
  `busena` tinyint(4) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `kodai`
--

INSERT INTO `kodai` (`id`, `kodas`, `zmogus`, `tipas`, `klase`, `parent`, `dalykas`, `busena`) VALUES
(1, 'OCGVQKLZSH', 2, 2, 0, 0, 1, 1),
(3, 'OEDJXVESGI', 4, 0, 9, 0, 0, 1),
(4, 'YMOWQNMTMG', 5, 2, 0, 0, 6, 1),
(5, 'HWPHDBZLDG', 6, 2, 0, 0, 9, 1),
(6, 'CQOVMMAAOV', 7, 0, 2, 0, 0, 1),
(7, 'ZCUNDUCHVG', 8, 1, 0, 7, 0, 1),
(8, 'LKRAUYJMLV', 9, 1, 0, 7, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `namu_darbai`
--

CREATE TABLE `namu_darbai` (
  `id` int(11) NOT NULL,
  `dalykas` int(11) NOT NULL,
  `klase` int(11) NOT NULL,
  `namu_darbas` text NOT NULL,
  `data` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `namu_darbai`
--

INSERT INTO `namu_darbai` (`id`, `dalykas`, `klase`, `namu_darbas`, `data`) VALUES
(2, 6, 2, 'Laba diena, asasd ,asd  ,sda sd,d .er.erer.g er,ge ,  er,er ,g ,g ,ge , ,e e ,a sasd asdasdasdas fr er g ger ge g   grgr  asd asda sdas da sd reg erge rgrgreergr  ergr grgr gr eg gr   gr gr', '2017-04-17'),
(3, 6, 3, 'assdadasdas 2', '2017-04-17'),
(5, 6, 2, 'asd', '2017-04-10'),
(7, 6, 2, 'Mokėti paaiškinti kuo kenksmingi sunkieji metalai,teršaluose esančios medžiagos.', '2017-04-04'),
(8, 6, 2, 'pisk naxui', '2017-04-22'),
(9, 9, 2, 'ffrfrfrfrfr', '2017-04-22'),
(10, 6, 2, 'asd2', '2016-11-02');

-- --------------------------------------------------------

--
-- Table structure for table `pazymiai`
--

CREATE TABLE `pazymiai` (
  `id` int(11) NOT NULL,
  `vartotojoid` int(11) NOT NULL,
  `dalykas` tinyint(4) NOT NULL,
  `pazymys` varchar(2) NOT NULL,
  `data` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `pazymiai`
--

INSERT INTO `pazymiai` (`id`, `vartotojoid`, `dalykas`, `pazymys`, `data`) VALUES
(1, 1, 2, '9', '2017-03-11'),
(2, 1, 1, '8', '2017-03-02'),
(5, 2, 6, '10', '2017-04-06'),
(7, 2, 6, '2', '2017-04-04'),
(9, 2, 6, 'n', '2017-04-20'),
(10, 2, 6, 'p', '2017-04-22'),
(11, 2, 9, '9', '2017-04-13'),
(14, 2, 9, '10', '2017-04-12'),
(15, 2, 9, 'n', '2017-02-09'),
(16, 2, 9, '8', '2017-02-16'),
(18, 6, 6, 'n', '2017-04-21');

-- --------------------------------------------------------

--
-- Table structure for table `vartotojai`
--

CREATE TABLE `vartotojai` (
  `id` int(11) NOT NULL,
  `zmogus` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `slaptazodis` char(64) NOT NULL,
  `tipas` tinyint(4) NOT NULL DEFAULT '0',
  `klase` int(11) NOT NULL DEFAULT '0',
  `parent` int(11) NOT NULL DEFAULT '0',
  `dalykas` tinyint(4) NOT NULL DEFAULT '0',
  `busena` tinyint(4) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `vartotojai`
--

INSERT INTO `vartotojai` (`id`, `zmogus`, `username`, `email`, `slaptazodis`, `tipas`, `klase`, `parent`, `dalykas`, `busena`) VALUES
(1, 1, 'admin', 'wascript3r@gmail.com', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 3, 0, 0, 0, 1),
(2, 4, 'algis', 'algis@gmail.com', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 0, 9, 0, 0, 1),
(3, 5, 'domantas', 'domantas@jankauskas.lt', '16ecab1875791e2b6ed0c9a6dae5a12a79d92120e1c3afbd3a9c8535ce44666d', 2, 0, 0, 6, 1),
(4, 6, 'marius', 'marius@kavaliauskas.lt', '16ecab1875791e2b6ed0c9a6dae5a12a79d92120e1c3afbd3a9c8535ce44666d', 2, 0, 0, 9, 1),
(5, 8, 'jonas', 'jonas@petraitis.lt', 'ed02457b5c41d964dbd2f2a609d63fe1bb7528dbe55e1abf5b52c249cd735797', 1, 0, 7, 0, 1),
(6, 7, 'petras', 'petras@petraitis.lt', '5fd924625f6ab16a19cc9807c7c506ae1813490e4ba675f843d5a10e0baacdb8', 0, 2, 0, 0, 1);

-- --------------------------------------------------------

--
-- Table structure for table `zmones`
--

CREATE TABLE `zmones` (
  `id` int(11) NOT NULL,
  `vardas` varchar(50) NOT NULL,
  `pavarde` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `zmones`
--

INSERT INTO `zmones` (`id`, `vardas`, `pavarde`) VALUES
(1, 'Mantas', 'Ramančionis'),
(2, 'Laimutė', 'Jocienė'),
(4, 'Algis', 'Jankauskas'),
(5, 'Domantas', 'Jankauskas'),
(6, 'Marius', 'Kavaliauskas'),
(7, 'Petras', 'Petraitis'),
(8, 'Jonas', 'Petraitis'),
(9, 'Aldona', 'Petraitienė');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `dalykai`
--
ALTER TABLE `dalykai`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `klases`
--
ALTER TABLE `klases`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kodai`
--
ALTER TABLE `kodai`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `namu_darbai`
--
ALTER TABLE `namu_darbai`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pazymiai`
--
ALTER TABLE `pazymiai`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `vartotojai`
--
ALTER TABLE `vartotojai`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `zmones`
--
ALTER TABLE `zmones`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `dalykai`
--
ALTER TABLE `dalykai`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
--
-- AUTO_INCREMENT for table `klases`
--
ALTER TABLE `klases`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT for table `kodai`
--
ALTER TABLE `kodai`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `namu_darbai`
--
ALTER TABLE `namu_darbai`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `pazymiai`
--
ALTER TABLE `pazymiai`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT for table `vartotojai`
--
ALTER TABLE `vartotojai`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `zmones`
--
ALTER TABLE `zmones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
