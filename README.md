#Sistema de Procesamiento y Auditoría de Facturación
Aplicación de escritorio desarrollada en C# con Windows Forms para una empresa regional de logística y entregas.

#Objetivo
Procesar lotes de facturas de flete capturados como cadenas de texto, validar la información de los paquetes, calcular tarifas, impuestos y recargos, almacenar las transacciones y exportar la auditoría a CSV.

Funcionalidades requeridas:
-Lectura de tarifas desde appsettings.json.
-Cambio dinámico de idioma entre español e inglés.
-Captura de lotes mediante texto multilínea.
-Validación y sanitización de cadenas.
-Almacenamiento temporal en arreglos nativos.
-Redimensionamiento manual de arreglos.
-Ordenamiento de paquetes por costo total.
-Cálculo de recargos por zona.
-Penalización por exceso de peso.
-Seguro obligatorio por valor declarado.
-Visualización de resultados en DataGridView.
-Inspección detallada del paquete seleccionado.
-Exportación de auditoría a CSV.
-Persistencia de transacciones en base de datos.
-Manejo de errores y excepciones.


#Reglas de negocio
Zona A: recargo del 5 %.
Zona B: recargo del 10 %.
Zona C: recargo del 18 %.
Más de 20 kg: penalización por cada kilogramo adicional.
Valor declarado superior a $1,000.00: seguro obligatorio del 2 %.
El IVA, precio por kilogramo y costo fijo se obtienen de appsettings.json.
