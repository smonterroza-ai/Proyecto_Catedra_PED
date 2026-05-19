    use master

    go
    drop database if exists  Reise2
 
    create database Reise2
    go
    use Reise2
    go

    -- ==========================================
    -- 1. CREACIÓN DE TABLAS (SIN RELACIONES AÚN)
    -- ==========================================

    create table Empresa (
        IdEmpresa int not null,
        NombreEmpresa varchar(20)
    )
    go

    create table RutaBuses (
        IdRutaBuses int not null,
        IdEmpresa int not null,
        IdRecorridoRuta int not null,
        CostoDelPasaje decimal(10,2) -- CORREGIDO
    )
    go

    create table InfoRutaBuses (
        IdRutaBuses int not null,
        NumeroRuta varchar(8),
        MotoristaNombre varchar(150),
        IdDetalleBuses int not null
    )
    go

    create table DetalleBuses (
        IdDetalleBuses int not null,
        Marca varchar(20),
        Modelo varchar(20),
        EstadoVehiculo varchar(20),
        PlacaVehiculo varchar(8),
        IdTipoVehiculo int not null
    )
    go

    create table TipoVehiculo (
        IdTipoVehiculo int not null,
        TipoVehiculo varchar(15)
    )
    go

    create table RecorridoRuta (
        IdRecorridoRuta int not null,
        inicio varchar(20),
        Final varchar(20)
    )
    go

    create table InfoRecorridoRuta (
        IdRecorridoRuta int not null,
        ParadasRuta varchar(500),
        CoordenadasGPS VARCHAR(MAX) -- GPS AÑADIDO CORRECTAMENTE
    )
    go

    create table CompraPasajes (
        IdCompraPasajes int not null,
        IdRutaBuses int not null,
        CantidadAComprar int,
        TotalApagar decimal(10,2) -- CORREGIDO	
    )
    go

    create table DetalleVenta (
        IdDetalleVenta int not null,
        IdCompraPasajes int not null,
        IdCliente int not null,
        IdMetodosDePago int not null,
        Hora datetime,
        Estado varchar(10)
    )
    go

    create table MetodosDePago (
        IdMetodosDePago int not null,
        NombreMetodosDePago varchar(20)
    )
    go

    create table Cliente (
        IdCliente int not null
    )
    go

    create table InfoCliente (
        IdCliente int not null,
        Nombre varchar(10),
        Apellido varchar(10),
        Correo varchar(100),
        Contraseña varchar(200),
        Saldo DECIMAL(10,2) NOT NULL
    )
    go


    -- ====== TUS TABLAS MODIFICADAS ======

    create table InfoEmpleado (
        IdEmpleado int identity(1,1) not null, -- AUTOGENERADO
        Nombre varchar(150),
        DUI VARCHAR(10) NOT NULL UNIQUE,
        FechaNacimiento DATE NOT NULL,
        Direccion VARCHAR(200) NOT NULL,
        Telefono VARCHAR(15) NOT NULL UNIQUE,
        FechaContratacion DATE DEFAULT GETDATE(),
        Correo varchar(100) NOT NULL UNIQUE,
        Usuario varchar(20) NOT NULL UNIQUE,
        Contraseña varchar(200) NOT NULL 
    )
    go

    create table RolEmpleado (
        IdRolEmpleado int identity(1,1) not null, -- AUTOGENERADO
        Roles varchar(15)
    )
    go

    create table Empleado (
        IdEmpleado int not null,
        IdRolEmpleado int not null
    )
    go


    -- ==========================================
    -- 2. CREACIÓN DE LLAVES PRIMARIAS
    -- ==========================================

    alter table RolEmpleado add constraint PK_IdRolEmpleado primary key(IdRolEmpleado)
    alter table InfoEmpleado add constraint PK_IdInfoEmpleado primary key(IdEmpleado)
    alter table Empleado add constraint PK_IdEmpleado primary key(IdEmpleado)
    alter table Empresa add constraint PK_IdEmpresa primary key(IdEmpresa)
    alter table cliente add constraint PK_IdCliente primary key(IdCliente)
    alter table MetodosDePago add CONSTRAINT Pk_IdMetodosDePago primary key(IdMetodosDePago)
    alter table DetalleVenta add constraint Pk_IdDetalleVenta primary key(IdDetalleVenta)
    alter table CompraPasajes add constraint Pk_IdCompraPasajes primary key(IdCompraPasajes)
    alter table RutaBuses add constraint PK_IdRutaBuses primary key (IdRutaBuses)
    alter table TipoVehiculo add constraint PK_idTipoVehiculo primary key(IdTipoVehiculo)
    alter table RecorridoRuta add constraint PK_IdRecorridoRuta primary key(IdRecorridoRuta)
    alter table DetalleBuses add constraint Pk_IdDetalleBuses primary key(IdDetalleBuses)
    go

    -- ==========================================
    -- 3. CREACIÓN DE LLAVES FORÁNEAS (RELACIONES)
    -- ==========================================

    -- Relaciones de Empleados (Corregidas para tu nueva lógica)
    alter table Empleado add constraint FK_IdEmpleadoInfo foreign key(IdEmpleado) references InfoEmpleado(IdEmpleado)
    alter table Empleado add constraint FK_IDRolEmpleado foreign key(IdRolEmpleado) references RolEmpleado(IdRolEmpleado)

    -- Relaciones Restantes
    alter table RutaBuses add constraint FK_IdEmpresa foreign key(IdEmpresa) references Empresa(IdEmpresa)
    alter table RutaBuses add constraint FK_IdRecorridoRuta foreign key(IdRecorridoRuta) references RecorridoRuta(IdRecorridoRuta)
    alter table CompraPasajes add constraint FK_IdRutaBuses foreign key(IdRutaBuses) references RutaBuses(IdRutaBuses)
    alter table DetalleVenta add constraint FK_IdCompraPasajes foreign key(IdCompraPasajes) references CompraPasajes(IdCompraPasajes)
    alter table DetalleVenta add constraint FK_IdCliente foreign key(IdCliente) references Cliente(IdCliente)
    alter table DetalleVenta add constraint FK_IdMetodosDePago foreign key(IdMetodosDePago) references MetodosDePago(IdMetodosDePago)
    alter table InfoRutaBuses add constraint FK_IdRutaBuses2 foreign key(IdRutaBuses) references RutaBuses(IdRutaBuses)
    alter table InfoRutaBuses add constraint FK_IdDetalleBuses foreign key(IdDetalleBuses) references DetalleBuses(IdDetalleBuses)
    alter table DetalleBuses add constraint FK_IdTipoVehiculo foreign key(IdTipoVehiculo) references TipoVehiculo(IdTipoVehiculo)
    alter table InfoRecorridoRuta add constraint FK_IdRecorridoRuta2 foreign key(IdRecorridoRuta) references RecorridoRuta(IdRecorridoRuta)
    alter table InfoCliente add constraint Fk_IdCliente2 foreign key(IdCliente) references Cliente(IdCliente)
    go

    -- ==========================================
    -- 4. INSERCIÓN DE DATOS INICIALES
    -- ==========================================

    -- Como usaste IDENTITY(1,1), no tienes que poner el ID, SQL lo pone solo:
    INSERT INTO RolEmpleado (Roles) VALUES ('Administrador');
    INSERT INTO RolEmpleado (Roles) VALUES ('Conductor');
    GO

    -- 1. Creamos la Empresa 
    INSERT INTO Empresa (IdEmpresa, NombreEmpresa) 
    VALUES (1, 'Transportes Reise');

    -- 2. Creamos un Tipo de Vehículo
    INSERT INTO TipoVehiculo (IdTipoVehiculo, TipoVehiculo) 
    VALUES (1, 'Autobús');
    INSERT INTO TipoVehiculo (IdTipoVehiculo, TipoVehiculo) 
    VALUES (2, 'Microbus');
    IF NOT EXISTS (SELECT 1 FROM DetalleBuses WHERE IdDetalleBuses = 1)
        INSERT INTO DetalleBuses (IdDetalleBuses, Marca, Modelo, EstadoVehiculo, PlacaVehiculo, IdTipoVehiculo) 
        VALUES (1, 'Predeterminado', 'Reise', 'Activo', 'AB-0000', 1);
    GO

    -- 4. Creamos los Métodos de Pago base
    INSERT INTO MetodosDePago (IdMetodosDePago, NombreMetodosDePago) VALUES (1, 'Tarjeta Reise');
    INSERT INTO MetodosDePago (IdMetodosDePago, NombreMetodosDePago) VALUES (2, 'Efectivo');
    GO