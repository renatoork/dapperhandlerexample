create table MGGLO.GLO_ENUMBOOLPERFORMANCE
(
  id           NUMBER(7) not null,
  codigo       NUMBER(7) not null,
  descricao    VARCHAR2(255) not null,
  entregue     CHAR(1) not null,
  liberado     CHAR(1),
  situacao     VARCHAR2(1) not null,
  situacaonula VARCHAR2(1),
  status       VARCHAR2(2) not null,
  statusnulo   VARCHAR2(2),
  valor        NUMBER(22,2) not null
);

declare
  x number;

begin
  execute immediate 'truncate table MGGLO.GLO_ENUMBOOLPERFORMANCE';

  for x in 1..1000000
  loop
    insert
      into MGGLO.GLO_ENUMBOOLPERFORMANCE(ID,
                              CODIGO,
                              DESCRICAO,
                              ENTREGUE,
                              LIBERADO,
                              SITUACAO,
                              SITUACAONULA,
                              STATUS,
                              STATUSNULO,
                              VALOR)
                       values(x,
                              x,
                              'Descricao',
                              decode(mod(x,2), 0, 'S', 'N'),
                              decode(mod(x,3), 0, 'S', 1, 'N', null),
                              decode(mod(x,4), 0, 'A', 1, 'C', 2, 'E', 'P'),
                              decode(mod(x,5), 0, 'A', 1, 'C', 2, 'E', 3, 'P', null),
                              decode(mod(x,4), 0, 'FE', 1, 'CO', 2, 'EP', 'TE'),
                              decode(mod(x,5), 0, 'FE', 1, 'CO', 2, 'EP', 3, 'TE', null),
                              mod(x,10000)/100);
    commit;
  end loop;
end;
/