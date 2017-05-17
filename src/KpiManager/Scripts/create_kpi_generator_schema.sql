CREATE DATABASE metric;
use metric;

CREATE TABLE pointofsale (
pointofsale_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY
);

CREATE TABLE location (
location_id VARCHAR(36) NOT NULL PRIMARY KEY
);

CREATE TABLE data_type (
data_type_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
data_type VARCHAR(30) NOT NULL,
is_numeric BOOLEAN NOT NULL,
float_precision TINYINT UNSIGNED NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
is_list tinyint(1) unsigned default 0
);

CREATE TABLE data_category (
data_category_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
category VARCHAR(50) NOT NULL,
description VARCHAR(255) NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL
);

CREATE TABLE operator (
operator_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
operator VARCHAR(50) NOT NULL,
operator_type VARCHAR(50) NOT NULL,
symbol VARCHAR(10),
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL
);

CREATE TABLE kpi (
kpi_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
name VARCHAR(50) NOT NULL,
description VARCHAR(255) NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL
);

CREATE TABLE metric_formula (
metric_formula_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
description VARCHAR(255) NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL
);

CREATE TABLE data_node (
data_node_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
node_name VARCHAR(30) NOT NULL,
pointofsale_id INT(6) UNSIGNED NOT NULL,
data_category_id INT(6) UNSIGNED NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_node_pos(pointofsale_id)
   REFERENCES pointofsale(pointofsale_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_node_category(data_category_id)
   REFERENCES data_category(data_category_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT
);

CREATE TABLE data_field_relationship (
data_field_relationship_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
field_source_id INT(6) UNSIGNED NOT NULL,
field_target_id INT(6) UNSIGNED NOT NULL
);

CREATE TABLE data_field (
data_field_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
data_field_name VARCHAR(255) NOT NULL,
data_type_id INT(6) UNSIGNED NOT NULL,
data_node_id INT(6) UNSIGNED NOT NULL,
data_field_relationship_id INT(6) UNSIGNED NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_field_datatype(data_type_id)
   REFERENCES data_type(data_type_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_field_node(data_node_id)
   REFERENCES data_node(data_node_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_data_field_relationship(data_field_relationship_id)
	REFERENCES data_field_relationship(data_field_relationship_id)
	ON UPDATE CASCADE
	ON DELETE RESTRICT
);

ALTER TABLE data_field_relationship
ADD FOREIGN KEY fk_relationship_source_field(field_source_id)
   REFERENCES data_field(data_field_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT;    
ALTER TABLE data_field_relationship
ADD FOREIGN KEY fk_relationship_target_field(field_target_id)
   REFERENCES data_field(data_field_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT;
   
CREATE TABLE operand (
operand_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
operand_value VARCHAR(30) NULL,
operand_data_field_id INT(6) UNSIGNED NULL,
operand_data_type_id INT(6) UNSIGNED NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_operand_field(operand_data_field_id)
   REFERENCES data_field(data_field_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_operand_data_type(operand_data_type_id)
   REFERENCES data_type(data_type_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT   
);

CREATE TABLE kpi_strategy (
kpi_strategy_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
description VARCHAR(255) NOT NULL,
kpi_id INT(6) UNSIGNED NOT NULL,
pointofsale_id INT(6) UNSIGNED NOT NULL,
is_system BOOLEAN NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_strategy_kpi(kpi_id)
   REFERENCES kpi(kpi_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_strategy_pos(pointofsale_id)
   REFERENCES pointofsale(pointofsale_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT
);

CREATE TABLE kpi_location_strategy (
kpi_strategy_id INT(6) UNSIGNED NOT NULL,
kpi_id INT(6) UNSIGNED NOT NULL,
location_id VARCHAR(36) NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_location_strategy(kpi_strategy_id)
   REFERENCES kpi_strategy(kpi_strategy_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_location_kpi(kpi_id)
   REFERENCES kpi(kpi_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_location_strategy_location(location_id)
   REFERENCES location(location_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
CONSTRAINT uq_kpi_location_id UNIQUE (kpi_id, location_id)
);

CREATE TABLE metric (
metric_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
metric VARCHAR(255) NOT NULL,
default_result_value VARCHAR(30) NULL,
result_data_type_id INT(6) UNSIGNED NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_metric_data_type(result_data_type_id)
   REFERENCES data_type(data_type_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT
);

CREATE TABLE metric_scheme (
metric_scheme_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
kpi_strategy_id INT(6) UNSIGNED NOT NULL,
metric_id INT(6) UNSIGNED NOT NULL,
operator_id INT(6) UNSIGNED NOT NULL,
operation_order INT(6) UNSIGNED NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_metric_scheme_strategy(kpi_strategy_id)
   REFERENCES kpi_strategy(kpi_strategy_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_metric_scheme_metric(metric_id)
   REFERENCES metric(metric_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_metric_scheme_operator(operator_id)
   REFERENCES operator(operator_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT   
);   

CREATE TABLE metric_formula_scheme (
metric_formula_scheme_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
metric_id INT(6) UNSIGNED NOT NULL,
metric_formula_id INT(6) UNSIGNED NOT NULL,
formula_order INT(6) UNSIGNED NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_formula_scheme_formula(metric_formula_id)
   REFERENCES metric_formula(metric_formula_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_formula_scheme_metric(metric_id)
   REFERENCES metric(metric_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT   
);

CREATE TABLE formula_operation (
formula_operation_id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
metric_formula_id INT(6) UNSIGNED NOT NULL,
operand_id INT(6) UNSIGNED NOT NULL,
operator_id INT(6) UNSIGNED NOT NULL,
operation_order INT(6) UNSIGNED NOT NULL,
modified_date DATETIME NULL,
created_date DATETIME NOT NULL,
modified_by VARCHAR(36) NULL,
created_by VARCHAR(36) NOT NULL,
FOREIGN KEY fk_formula_operation_metric_formula(metric_formula_id)
   REFERENCES metric_formula(metric_formula_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_formula_operation_operand(operand_id)
   REFERENCES operand(operand_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT,
FOREIGN KEY fk_formula_operation_operator(operator_id)
   REFERENCES operator(operator_id)
   ON UPDATE CASCADE
   ON DELETE RESTRICT   
)

