-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 08. Mai 2019 um 16:40
-- Server-Version: 10.1.37-MariaDB
-- PHP-Version: 7.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `it_sys_speisekarte`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `speisekarte`
--

CREATE TABLE `speisekarte` (
  `ID` int(11) NOT NULL,
  `Titel` text COLLATE utf8_unicode_ci NOT NULL,
  `Preis` text COLLATE utf8_unicode_ci NOT NULL,
  `Beschreibung` text COLLATE utf8_unicode_ci NOT NULL,
  `SpeisenArt_ID` int(11) NOT NULL,
  `BildRawData` longblob NOT NULL,
  `BildHeight` int(11) NOT NULL,
  `BildWidth` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `speisenart`
--

CREATE TABLE `speisenart` (
  `ID` int(11) NOT NULL,
  `SpeisenArtName` text COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `speisekarte`
--
ALTER TABLE `speisekarte`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `speisenart`
--
ALTER TABLE `speisenart`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `speisekarte`
--
ALTER TABLE `speisekarte`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `speisenart`
--
ALTER TABLE `speisenart`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
