using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KpiManager
{
    public partial class MetricContext : DbContext
    {
        public MetricContext(DbContextOptions<MetricContext> options) : base(options)
        {
        }

        public virtual DbSet<DataCategory> DataCategory { get; set; }
        public virtual DbSet<DataField> DataField { get; set; }
        public virtual DbSet<DataFieldRelationship> DataFieldRelationship { get; set; }
        public virtual DbSet<DataNode> DataNode { get; set; }
        public virtual DbSet<DataType> DataType { get; set; }
        public virtual DbSet<FormulaOperation> FormulaOperation { get; set; }
        public virtual DbSet<Kpi> Kpi { get; set; }
        public virtual DbSet<KpiLocationStrategy> KpiLocationStrategy { get; set; }
        public virtual DbSet<KpiStrategy> KpiStrategy { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Metric> Metric { get; set; }
        public virtual DbSet<MetricFormula> MetricFormula { get; set; }
        public virtual DbSet<MetricFormulaScheme> MetricFormulaScheme { get; set; }
        public virtual DbSet<MetricScheme> MetricScheme { get; set; }
        public virtual DbSet<Operand> Operand { get; set; }
        public virtual DbSet<Operator> Operator { get; set; }
        public virtual DbSet<Pointofsale> Pointofsale { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataCategory>(entity =>
            {
                entity.ToTable("data_category");

                entity.Property(e => e.DataCategoryId)
                    .HasColumnName("data_category_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<DataField>(entity =>
            {
                entity.ToTable("data_field");

                entity.HasIndex(e => e.DataFieldRelationshipId)
                    .HasName("fk_data_field_relationship");

                entity.HasIndex(e => e.DataNodeId)
                    .HasName("fk_field_node");

                entity.HasIndex(e => e.DataTypeId)
                    .HasName("fk_field_datatype");

                entity.Property(e => e.DataFieldId)
                    .HasColumnName("data_field_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataFieldName)
                    .IsRequired()
                    .HasColumnName("data_field_name")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.DataFieldRelationshipId)
                    .HasColumnName("data_field_relationship_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.DataNodeId)
                    .HasColumnName("data_node_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.DataTypeId)
                    .HasColumnName("data_type_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.DataFieldRelationship)
                    .WithMany(p => p.DataField)
                    .HasForeignKey(d => d.DataFieldRelationshipId)
                    .HasConstraintName("data_field_ibfk_3");

                entity.HasOne(d => d.DataNode)
                    .WithMany(p => p.DataField)
                    .HasForeignKey(d => d.DataNodeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("data_field_ibfk_2");

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.DataField)
                    .HasForeignKey(d => d.DataTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("data_field_ibfk_1");
            });

            modelBuilder.Entity<DataFieldRelationship>(entity =>
            {
                entity.ToTable("data_field_relationship");

                entity.HasIndex(e => e.FieldSourceId)
                    .HasName("fk_relationship_source_field");

                entity.HasIndex(e => e.FieldTargetId)
                    .HasName("fk_relationship_target_field");

                entity.Property(e => e.DataFieldRelationshipId)
                    .HasColumnName("data_field_relationship_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.FieldSourceId)
                    .HasColumnName("field_source_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.FieldTargetId)
                    .HasColumnName("field_target_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.FieldSource)
                    .WithMany(p => p.DataFieldRelationshipFieldSource)
                    .HasForeignKey(d => d.FieldSourceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("data_field_relationship_ibfk_1");

                entity.HasOne(d => d.FieldTarget)
                    .WithMany(p => p.DataFieldRelationshipFieldTarget)
                    .HasForeignKey(d => d.FieldTargetId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("data_field_relationship_ibfk_2");
            });

            modelBuilder.Entity<DataNode>(entity =>
            {
                entity.ToTable("data_node");

                entity.HasIndex(e => e.DataCategoryId)
                    .HasName("fk_node_category");

                entity.HasIndex(e => e.PointofsaleId)
                    .HasName("fk_node_pos");

                entity.Property(e => e.DataNodeId)
                    .HasColumnName("data_node_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataCategoryId)
                    .HasColumnName("data_category_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NodeName)
                    .IsRequired()
                    .HasColumnName("node_name")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.PointofsaleId)
                    .HasColumnName("pointofsale_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.DataCategory)
                    .WithMany(p => p.DataNode)
                    .HasForeignKey(d => d.DataCategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("data_node_ibfk_2");

                entity.HasOne(d => d.Pointofsale)
                    .WithMany(p => p.DataNode)
                    .HasForeignKey(d => d.PointofsaleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("data_node_ibfk_1");
            });

            modelBuilder.Entity<DataType>(entity =>
            {
                entity.ToTable("data_type");

                entity.Property(e => e.DataTypeId)
                    .HasColumnName("data_type_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataType1)
                    .IsRequired()
                    .HasColumnName("data_type")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.FloatPrecision)
                    .HasColumnName("float_precision")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.IsNumeric)
                    .HasColumnName("is_numeric")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FormulaOperation>(entity =>
            {
                entity.ToTable("formula_operation");

                entity.HasIndex(e => e.MetricFormulaId)
                    .HasName("fk_formula_operation_metric_formula");

                entity.HasIndex(e => e.OperandId)
                    .HasName("fk_formula_operation_operand");

                entity.HasIndex(e => e.OperatorId)
                    .HasName("fk_formula_operation_operator");

                entity.Property(e => e.FormulaOperationId)
                    .HasColumnName("formula_operation_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MetricFormulaId)
                    .HasColumnName("metric_formula_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperandId)
                    .HasColumnName("operand_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.OperationOrder)
                    .HasColumnName("operation_order")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("operator_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.MetricFormula)
                    .WithMany(p => p.FormulaOperation)
                    .HasForeignKey(d => d.MetricFormulaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("formula_operation_ibfk_1");

                entity.HasOne(d => d.Operand)
                    .WithMany(p => p.FormulaOperation)
                    .HasForeignKey(d => d.OperandId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("formula_operation_ibfk_2");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.FormulaOperation)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("formula_operation_ibfk_3");
            });

            modelBuilder.Entity<Kpi>(entity =>
            {
                entity.ToTable("kpi");

                entity.Property(e => e.KpiId)
                    .HasColumnName("kpi_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<KpiLocationStrategy>(entity =>
            {
                entity.HasKey(e => new { e.KpiId, e.LocationId })
                    .HasName("uq_kpi_location_id");

                entity.ToTable("kpi_location_strategy");

                entity.HasIndex(e => e.KpiStrategyId)
                    .HasName("fk_location_strategy");

                entity.HasIndex(e => e.LocationId)
                    .HasName("fk_location_strategy_location");

                entity.Property(e => e.KpiId)
                    .HasColumnName("kpi_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.KpiStrategyId)
                    .HasColumnName("kpi_strategy_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Kpi)
                    .WithMany(p => p.KpiLocationStrategy)
                    .HasForeignKey(d => d.KpiId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("kpi_location_strategy_ibfk_2");

                entity.HasOne(d => d.KpiStrategy)
                    .WithMany(p => p.KpiLocationStrategy)
                    .HasForeignKey(d => d.KpiStrategyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("kpi_location_strategy_ibfk_1");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.KpiLocationStrategy)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("kpi_location_strategy_ibfk_3");
            });

            modelBuilder.Entity<KpiStrategy>(entity =>
            {
                entity.ToTable("kpi_strategy");

                entity.HasIndex(e => e.KpiId)
                    .HasName("fk_strategy_kpi");

                entity.HasIndex(e => e.PointofsaleId)
                    .HasName("fk_strategy_pos");

                entity.Property(e => e.KpiStrategyId)
                    .HasColumnName("kpi_strategy_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.IsSystem)
                    .HasColumnName("is_system")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.KpiId)
                    .HasColumnName("kpi_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PointofsaleId)
                    .HasColumnName("pointofsale_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.Kpi)
                    .WithMany(p => p.KpiStrategy)
                    .HasForeignKey(d => d.KpiId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("kpi_strategy_ibfk_1");

                entity.HasOne(d => d.Pointofsale)
                    .WithMany(p => p.KpiStrategy)
                    .HasForeignKey(d => d.PointofsaleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("kpi_strategy_ibfk_2");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasColumnType("varchar(36)");
            });

            modelBuilder.Entity<Metric>(entity =>
            {
                entity.ToTable("metric");

                entity.HasIndex(e => e.ResultDataTypeId)
                    .HasName("fk_metric_data_type");

                entity.Property(e => e.MetricId)
                    .HasColumnName("metric_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DefaultResultValue)
                    .HasColumnName("default_result_value")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Metric1)
                    .IsRequired()
                    .HasColumnName("metric")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ResultDataTypeId)
                    .HasColumnName("result_data_type_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.ResultDataType)
                    .WithMany(p => p.Metric)
                    .HasForeignKey(d => d.ResultDataTypeId)
                    .HasConstraintName("metric_ibfk_1");
            });

            modelBuilder.Entity<MetricFormula>(entity =>
            {
                entity.ToTable("metric_formula");

                entity.Property(e => e.MetricFormulaId)
                    .HasColumnName("metric_formula_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<MetricFormulaScheme>(entity =>
            {
                entity.ToTable("metric_formula_scheme");

                entity.HasIndex(e => e.MetricId)
                    .HasName("fk_formula_scheme_metric");

                entity.HasIndex(e => e.MetricFormulaId)
                    .HasName("fk_formula_scheme_formula");

                entity.Property(e => e.MetricFormulaSchemeId)
                    .HasColumnName("metric_formula_scheme_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.FormulaOrder)
                    .IsRequired()
                    .HasColumnName("formula_order")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.MetricId)
                    .HasColumnName("metric_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.MetricFormulaId)
                    .IsRequired()
                    .HasColumnName("metric_formula_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.Metric)
                    .WithMany(p => p.MetricFormulaScheme)
                    .HasForeignKey(d => d.MetricId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("metric_formula_scheme_ibfk_2");

                entity.HasOne(d => d.MetricFormula)
                    .WithMany(p => p.MetricFormulaScheme)
                    .HasForeignKey(d => d.MetricFormulaId)
                    .HasConstraintName("metric_formula_scheme_ibfk_1");
            });

            modelBuilder.Entity<MetricScheme>(entity =>
            {
                entity.ToTable("metric_scheme");

                entity.HasIndex(e => e.KpiStrategyId)
                    .HasName("fk_metric_scheme_strategy");

                entity.HasIndex(e => e.MetricId)
                    .HasName("fk_metric_scheme_metric");

                entity.HasIndex(e => e.OperatorId)
                    .HasName("fk_metric_scheme_operator");

                entity.Property(e => e.MetricSchemeId)
                    .HasColumnName("metric_scheme_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.KpiStrategyId)
                    .HasColumnName("kpi_strategy_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.MetricId)
                    .HasColumnName("metric_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperationOrder)
                    .HasColumnName("operation_order")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("operator_id")
                    .HasColumnType("int(6) unsigned");

                entity.HasOne(d => d.KpiStrategy)
                    .WithMany(p => p.MetricScheme)
                    .HasForeignKey(d => d.KpiStrategyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("metric_scheme_ibfk_1");

                entity.HasOne(d => d.Metric)
                    .WithMany(p => p.MetricScheme)
                    .HasForeignKey(d => d.MetricId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("metric_scheme_ibfk_2");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.MetricScheme)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("metric_scheme_ibfk_3");
            });

            modelBuilder.Entity<Operand>(entity =>
            {
                entity.ToTable("operand");

                entity.HasIndex(e => e.OperandDataFieldId)
                    .HasName("fk_operand_field");

                entity.HasIndex(e => e.OperandDataTypeId)
                    .HasName("fk_operand_data_type");

                entity.Property(e => e.OperandId)
                    .HasColumnName("operand_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OperandDataFieldId)
                    .HasColumnName("operand_data_field_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.OperandDataTypeId)
                    .HasColumnName("operand_data_type_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.OperandValue)
                    .HasColumnName("operand_value")
                    .HasColumnType("varchar(30)");

                entity.HasOne(d => d.OperandDataField)
                    .WithMany(p => p.Operand)
                    .HasForeignKey(d => d.OperandDataFieldId)
                    .HasConstraintName("operand_ibfk_1");

                entity.HasOne(d => d.OperandDataType)
                    .WithMany(p => p.Operand)
                    .HasForeignKey(d => d.OperandDataTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("operand_ibfk_2");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("operator");

                entity.Property(e => e.OperatorId)
                    .HasColumnName("operator_id")
                    .HasColumnType("int(6) unsigned");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasColumnName("modified_by")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Operator1)
                    .IsRequired()
                    .HasColumnName("operator")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.OperatorType)
                    .IsRequired()
                    .HasColumnName("operator_type")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol")
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Pointofsale>(entity =>
            {
                entity.ToTable("pointofsale");

                entity.Property(e => e.PointofsaleId)
                    .HasColumnName("pointofsale_id")
                    .HasColumnType("int(6) unsigned");
            });
        }
    }
}