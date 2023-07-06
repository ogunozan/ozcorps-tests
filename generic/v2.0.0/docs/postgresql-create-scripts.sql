create sequence public.poi_seq;

CREATE TABLE IF NOT EXISTS public.poi
(
    id bigint,
    name text COLLATE pg_catalog."default",
    geoloc geometry(Geometry,4326),
    is_active boolean DEFAULT true,
    is_deleted boolean DEFAULT false,
    deleted_user_id bigint,
    delete_date timestamp(4) without time zone,
    inserted_user_id bigint,
    inserted_date timestamp(4) without time zone,
    modified_user_id bigint,
    modified_date timestamp(4) without time zone
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;