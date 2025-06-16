CREATE TABLE `Order` (
  `OrderId` varchar(100) NOT NULL,
  `PID` varchar(100) NOT NULL,
  `UserId` varchar(100) NOT NULL,
  `OrderStatus` int(11) NOT NULL,
  `DateCreated` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) 

CREATE TABLE `OrderDetails` (
  `OrderId` varchar(100) NOT NULL,
  `ProductId` varchar(100) NOT NULL,
  `ProductName` varchar(100) NOT NULL,
  `ProductImage` text NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL
)

CREATE TABLE `Product` (
  `ProductId` varchar(100) NOT NULL,
  `ProductName` varchar(100) NOT NULL,
  `ProductImage` text NOT NULL,
  `Price` decimal(10,0) NOT NULL
)

ALTER TABLE `Order`
ADD PRIMARY KEY (`OrderId`);


ALTER TABLE `OrderDetails`
ADD KEY `fk_orderid` (`OrderId`),
ADD KEY `fk_productId` (`ProductId`);


ALTER TABLE `Product`
ADD PRIMARY KEY (`ProductId`);

ALTER TABLE `OrderDetails`
ADD CONSTRAINT `fk_orderid` FOREIGN KEY (`OrderId`) REFERENCES `Order` (`OrderId`),
ADD CONSTRAINT `fk_productId` FOREIGN KEY (`ProductId`) REFERENCES `Product` (`ProductId`);
COMMIT;