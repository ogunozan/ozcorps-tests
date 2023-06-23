CREATE TABLE IF NOT EXISTS log_test.action_log
(
    id bigserial not null,
    date timestamp(4) without time zone,
    action_name text COLLATE pg_catalog."default",
    controller_name text COLLATE pg_catalog."default",
    full_name text COLLATE pg_catalog."default",
    request text COLLATE pg_catalog."default",
    user_id bigint,
    user_ip_address text COLLATE pg_catalog."default",
    user_roles text COLLATE pg_catalog."default",
    username text COLLATE pg_catalog."default",
    response text COLLATE pg_catalog."default",
    milli_seconds bigint,
    url text COLLATE pg_catalog."default",
    CONSTRAINT action_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS log_test.audit_log
(
    id bigserial NOT NULL,
    date timestamp(4) without time zone,
    user_id bigint,
    username text COLLATE pg_catalog."default",
    user_roles text COLLATE pg_catalog."default",
    json text COLLATE pg_catalog."default",
    entity text COLLATE pg_catalog."default",
    entity_id bigint,
    operation text COLLATE pg_catalog."default",
    geoloc text COLLATE pg_catalog."default",
    table_ text COLLATE pg_catalog."default",
    CONSTRAINT audit_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS log_test.other_log
(
    id bigserial NOT NULL,
    date timestamp(4) without time zone,
    type text COLLATE pg_catalog."default",
    exception text COLLATE pg_catalog."default",
    stack_trace text COLLATE pg_catalog."default",
    message text COLLATE pg_catalog."default",
    CONSTRAINT other_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS log_test.user_log
(
    id bigserial NOT NULL,
    date timestamp(4) without time zone,
    user_id bigint,
    username text COLLATE pg_catalog."default",
    user_roles text COLLATE pg_catalog."default",
    type text COLLATE pg_catalog."default",
    CONSTRAINT user_log_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;