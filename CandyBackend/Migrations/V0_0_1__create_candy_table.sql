create table candy
(
    id     bigserial
        constraint pk_candy primary key,
    name   text   not null,
    price  bigint not null
);