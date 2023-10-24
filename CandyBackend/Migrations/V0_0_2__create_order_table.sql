create table "order"
(
    id          bigserial
        constraint pk_order primary key,
    first_name  text   not null,
    last_name   text   not null,
    order_total bigint not null
);

create table order_item
(
    id          bigserial
        constraint pk_order_item primary key,
    order_id    bigint not null
        constraint fk_order_item_order_order_entity_id references "order" on delete cascade,
    position    bigint not null,
    description text   not null,
    price       bigint not null
);

create index ix_order_item_order_id
    on order_item (order_id);
