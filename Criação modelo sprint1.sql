CREATE TABLE Usuario(
	email VARCHAR(100) PRIMARY KEY DEFAULT 'desconhecido',
	nome VARCHAR(200) NOT NULL,
);

CREATE TABLE Cliente(
	nome VARCHAR(150) PRIMARY KEY
);

CREATE TABLE Area(
	nome VARCHAR(23) PRIMARY KEY DEFAULT 'desconhecida'
);

CREATE TABLE Projeto(
	idProjeto INT IDENTITY(1, 1) PRIMARY KEY,
	nome VARCHAR(100) NOT NULL,
	cliente VARCHAR(150),
	dataIni DATE NOT NULL,
	dataFim DATE,
	descricao VARCHAR(500) NOT NULL,
	area VARCHAR(23),
	FOREIGN KEY (cliente) REFERENCES Cliente(nome) ON UPDATE CASCADE ON DELETE SET NULL,
	FOREIGN KEY (area) REFERENCES Area(nome) ON UPDATE CASCADE ON DELETE SET DEFAULT
);

CREATE TABLE ProjetoUsuario(
	idProjeto INT,
	usuario VARCHAR(100),
	FOREIGN KEY (idProjeto) REFERENCES Projeto(idProjeto) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (usuario) REFERENCES Usuario(email) ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY (idProjeto, usuario)
);

CREATE TABLE Keyword(
	kw VARCHAR(100) PRIMARY KEY
);

CREATE TABLE Componente(
	idComponente INT IDENTITY(1, 1) PRIMARY KEY,
	nome VARCHAR(100) NOT NULL,
	usuario VARCHAR(100),
	projeto INT,
	FOREIGN KEY (projeto) REFERENCES Projeto(idProjeto) ON UPDATE CASCADE ON DELETE SET NULL,
	FOREIGN KEY (usuario) REFERENCES Usuario(email) ON UPDATE CASCADE ON DELETE SET DEFAULT
);

CREATE TABLE componente_kw(
	kw VARCHAR(100),
	idComponente INT,
	FOREIGN KEY (kw) REFERENCES Keyword(kw) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (idComponente) REFERENCES Componente(idComponente) ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY (idComponente, kw)
);

INSERT INTO Usuario VALUES ('camila@gmail.com', 'camila');
INSERT INTO Componente VALUES('nome1', 'camila@gmail.com', NULL);
INSERT INTO Componente VALUES('nome2', 'camila@gmail.com', NULL);
INSERT INTO Componente VALUES('nome3', 'camila@gmail.com', NULL);

select * from Componente;

