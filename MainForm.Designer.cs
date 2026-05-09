namespace DijkstraApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        
        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            this.graphPanel = new System.Windows.Forms.Panel();
            this.panelControls = new System.Windows.Forms.Panel();
            this.groupBoxVertex = new System.Windows.Forms.GroupBox();
            this.btnRemoveVertex = new System.Windows.Forms.Button();
            this.btnAddVertex = new System.Windows.Forms.Button();
            this.txtVertexId = new System.Windows.Forms.TextBox();
            this.lblVertexId = new System.Windows.Forms.Label();
            this.groupBoxEdge = new System.Windows.Forms.GroupBox();
            this.btnRemoveEdge = new System.Windows.Forms.Button();
            this.btnAddEdge = new System.Windows.Forms.Button();
            this.txtEdgeWeight = new System.Windows.Forms.TextBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.txtEdgeTo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtEdgeFrom = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.groupBoxPath = new System.Windows.Forms.GroupBox();
            this.btnFindPath = new System.Windows.Forms.Button();
            this.cmbDestVertex = new System.Windows.Forms.ComboBox();
            this.lblDestVertex = new System.Windows.Forms.Label();
            this.cmbSourceVertex = new System.Windows.Forms.ComboBox();
            this.lblSourceVertex = new System.Windows.Forms.Label();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnClearGraph = new System.Windows.Forms.Button();
            this.btnResetDefault = new System.Windows.Forms.Button();
            this.panelControls.SuspendLayout();
            this.groupBoxVertex.SuspendLayout();
            this.groupBoxEdge.SuspendLayout();
            this.groupBoxPath.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphPanel
            // 
            this.graphPanel.BackColor = System.Drawing.Color.White;
            this.graphPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphPanel.Location = new System.Drawing.Point(0, 0);
            this.graphPanel.Name = "graphPanel";
            this.graphPanel.Size = new System.Drawing.Size(784, 561);
            this.graphPanel.TabIndex = 0;
            this.graphPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphPanel_Paint);
            this.graphPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseDown);
            this.graphPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseMove);
            this.graphPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphPanel_MouseUp);
            // 
            // panelControls
            // 
            this.panelControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControls.Controls.Add(this.btnResetDefault);
            this.panelControls.Controls.Add(this.btnClearGraph);
            this.panelControls.Controls.Add(this.groupBoxResult);
            this.panelControls.Controls.Add(this.groupBoxPath);
            this.panelControls.Controls.Add(this.groupBoxEdge);
            this.panelControls.Controls.Add(this.groupBoxVertex);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(784, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(300, 561);
            this.panelControls.TabIndex = 1;
            // 
            // groupBoxVertex
            // 
            this.groupBoxVertex.Controls.Add(this.btnRemoveVertex);
            this.groupBoxVertex.Controls.Add(this.btnAddVertex);
            this.groupBoxVertex.Controls.Add(this.txtVertexId);
            this.groupBoxVertex.Controls.Add(this.lblVertexId);
            this.groupBoxVertex.Location = new System.Drawing.Point(10, 10);
            this.groupBoxVertex.Name = "groupBoxVertex";
            this.groupBoxVertex.Size = new System.Drawing.Size(280, 90);
            this.groupBoxVertex.TabIndex = 0;
            this.groupBoxVertex.TabStop = false;
            this.groupBoxVertex.Text = "Управление вершинами";
            // 
            // btnRemoveVertex
            // 
            this.btnRemoveVertex.Location = new System.Drawing.Point(145, 52);
            this.btnRemoveVertex.Name = "btnRemoveVertex";
            this.btnRemoveVertex.Size = new System.Drawing.Size(120, 25);
            this.btnRemoveVertex.TabIndex = 3;
            this.btnRemoveVertex.Text = "Удалить вершину";
            this.btnRemoveVertex.UseVisualStyleBackColor = true;
            this.btnRemoveVertex.Click += new System.EventHandler(this.btnRemoveVertex_Click);
            // 
            // btnAddVertex
            // 
            this.btnAddVertex.Location = new System.Drawing.Point(15, 52);
            this.btnAddVertex.Name = "btnAddVertex";
            this.btnAddVertex.Size = new System.Drawing.Size(120, 25);
            this.btnAddVertex.TabIndex = 2;
            this.btnAddVertex.Text = "Добавить вершину";
            this.btnAddVertex.UseVisualStyleBackColor = true;
            this.btnAddVertex.Click += new System.EventHandler(this.btnAddVertex_Click);
            // 
            // txtVertexId
            // 
            this.txtVertexId.Location = new System.Drawing.Point(115, 22);
            this.txtVertexId.Name = "txtVertexId";
            this.txtVertexId.Size = new System.Drawing.Size(150, 20);
            this.txtVertexId.TabIndex = 1;
            // 
            // lblVertexId
            // 
            this.lblVertexId.AutoSize = true;
            this.lblVertexId.Location = new System.Drawing.Point(15, 25);
            this.lblVertexId.Name = "lblVertexId";
            this.lblVertexId.Size = new System.Drawing.Size(69, 13);
            this.lblVertexId.TabIndex = 0;
            this.lblVertexId.Text = "ID вершины:";
            // 
            // groupBoxEdge
            // 
            this.groupBoxEdge.Controls.Add(this.btnRemoveEdge);
            this.groupBoxEdge.Controls.Add(this.btnAddEdge);
            this.groupBoxEdge.Controls.Add(this.txtEdgeWeight);
            this.groupBoxEdge.Controls.Add(this.lblWeight);
            this.groupBoxEdge.Controls.Add(this.txtEdgeTo);
            this.groupBoxEdge.Controls.Add(this.lblTo);
            this.groupBoxEdge.Controls.Add(this.txtEdgeFrom);
            this.groupBoxEdge.Controls.Add(this.lblFrom);
            this.groupBoxEdge.Location = new System.Drawing.Point(10, 110);
            this.groupBoxEdge.Name = "groupBoxEdge";
            this.groupBoxEdge.Size = new System.Drawing.Size(280, 140);
            this.groupBoxEdge.TabIndex = 1;
            this.groupBoxEdge.TabStop = false;
            this.groupBoxEdge.Text = "Управление рёбрами";
            // 
            // btnRemoveEdge
            // 
            this.btnRemoveEdge.Location = new System.Drawing.Point(145, 105);
            this.btnRemoveEdge.Name = "btnRemoveEdge";
            this.btnRemoveEdge.Size = new System.Drawing.Size(120, 25);
            this.btnRemoveEdge.TabIndex = 7;
            this.btnRemoveEdge.Text = "Удалить ребро";
            this.btnRemoveEdge.UseVisualStyleBackColor = true;
            this.btnRemoveEdge.Click += new System.EventHandler(this.btnRemoveEdge_Click);
            // 
            // btnAddEdge
            // 
            this.btnAddEdge.Location = new System.Drawing.Point(15, 105);
            this.btnAddEdge.Name = "btnAddEdge";
            this.btnAddEdge.Size = new System.Drawing.Size(120, 25);
            this.btnAddEdge.TabIndex = 6;
            this.btnAddEdge.Text = "Добавить ребро";
            this.btnAddEdge.UseVisualStyleBackColor = true;
            this.btnAddEdge.Click += new System.EventHandler(this.btnAddEdge_Click);
            // 
            // txtEdgeWeight
            // 
            this.txtEdgeWeight.Location = new System.Drawing.Point(115, 75);
            this.txtEdgeWeight.Name = "txtEdgeWeight";
            this.txtEdgeWeight.Size = new System.Drawing.Size(150, 20);
            this.txtEdgeWeight.TabIndex = 5;
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Location = new System.Drawing.Point(15, 78);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(29, 13);
            this.lblWeight.TabIndex = 4;
            this.lblWeight.Text = "Вес:";
            // 
            // txtEdgeTo
            // 
            this.txtEdgeTo.Location = new System.Drawing.Point(115, 48);
            this.txtEdgeTo.Name = "txtEdgeTo";
            this.txtEdgeTo.Size = new System.Drawing.Size(150, 20);
            this.txtEdgeTo.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(15, 51);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(21, 13);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "В:";
            // 
            // txtEdgeFrom
            // 
            this.txtEdgeFrom.Location = new System.Drawing.Point(115, 22);
            this.txtEdgeFrom.Name = "txtEdgeFrom";
            this.txtEdgeFrom.Size = new System.Drawing.Size(150, 20);
            this.txtEdgeFrom.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(15, 25);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(21, 13);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Из:";
            // 
            // groupBoxPath
            // 
            this.groupBoxPath.Controls.Add(this.btnFindPath);
            this.groupBoxPath.Controls.Add(this.cmbDestVertex);
            this.groupBoxPath.Controls.Add(this.lblDestVertex);
            this.groupBoxPath.Controls.Add(this.cmbSourceVertex);
            this.groupBoxPath.Controls.Add(this.lblSourceVertex);
            this.groupBoxPath.Location = new System.Drawing.Point(10, 260);
            this.groupBoxPath.Name = "groupBoxPath";
            this.groupBoxPath.Size = new System.Drawing.Size(280, 115);
            this.groupBoxPath.TabIndex = 2;
            this.groupBoxPath.TabStop = false;
            this.groupBoxPath.Text = "Поиск кратчайшего пути";
            // 
            // btnFindPath
            // 
            this.btnFindPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFindPath.Location = new System.Drawing.Point(15, 78);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(250, 28);
            this.btnFindPath.TabIndex = 4;
            this.btnFindPath.Text = "Найти путь";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);
            // 
            // cmbDestVertex
            // 
            this.cmbDestVertex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestVertex.FormattingEnabled = true;
            this.cmbDestVertex.Location = new System.Drawing.Point(115, 48);
            this.cmbDestVertex.Name = "cmbDestVertex";
            this.cmbDestVertex.Size = new System.Drawing.Size(150, 21);
            this.cmbDestVertex.TabIndex = 3;
            // 
            // lblDestVertex
            // 
            this.lblDestVertex.AutoSize = true;
            this.lblDestVertex.Location = new System.Drawing.Point(15, 51);
            this.lblDestVertex.Name = "lblDestVertex";
            this.lblDestVertex.Size = new System.Drawing.Size(64, 13);
            this.lblDestVertex.TabIndex = 2;
            this.lblDestVertex.Text = "Конечная:";
            // 
            // cmbSourceVertex
            // 
            this.cmbSourceVertex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceVertex.FormattingEnabled = true;
            this.cmbSourceVertex.Location = new System.Drawing.Point(115, 22);
            this.cmbSourceVertex.Name = "cmbSourceVertex";
            this.cmbSourceVertex.Size = new System.Drawing.Size(150, 21);
            this.cmbSourceVertex.TabIndex = 1;
            // 
            // lblSourceVertex
            // 
            this.lblSourceVertex.AutoSize = true;
            this.lblSourceVertex.Location = new System.Drawing.Point(15, 25);
            this.lblSourceVertex.Name = "lblSourceVertex";
            this.lblSourceVertex.Size = new System.Drawing.Size(71, 13);
            this.lblSourceVertex.TabIndex = 0;
            this.lblSourceVertex.Text = "Начальная:";
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.txtResult);
            this.groupBoxResult.Location = new System.Drawing.Point(10, 385);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(280, 100);
            this.groupBoxResult.TabIndex = 3;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Результат";
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.White;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(3, 16);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(274, 81);
            this.txtResult.TabIndex = 0;
            // 
            // btnClearGraph
            // 
            this.btnClearGraph.Location = new System.Drawing.Point(10, 495);
            this.btnClearGraph.Name = "btnClearGraph";
            this.btnClearGraph.Size = new System.Drawing.Size(280, 28);
            this.btnClearGraph.TabIndex = 4;
            this.btnClearGraph.Text = "Очистить граф";
            this.btnClearGraph.UseVisualStyleBackColor = true;
            this.btnClearGraph.Click += new System.EventHandler(this.btnClearGraph_Click);
            // 
            // btnResetDefault
            // 
            this.btnResetDefault.Location = new System.Drawing.Point(10, 527);
            this.btnResetDefault.Name = "btnResetDefault";
            this.btnResetDefault.Size = new System.Drawing.Size(280, 28);
            this.btnResetDefault.TabIndex = 5;
            this.btnResetDefault.Text = "Загрузить стандартный граф";
            this.btnResetDefault.UseVisualStyleBackColor = true;
            this.btnResetDefault.Click += new System.EventHandler(this.btnResetDefault_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.graphPanel);
            this.Controls.Add(this.panelControls);
            this.MinimumSize = new System.Drawing.Size(1100, 600);
            this.Name = "MainForm";
            this.Text = "Алгоритм Дейкстры - Поиск кратчайшего пути";
            this.panelControls.ResumeLayout(false);
            this.groupBoxVertex.ResumeLayout(false);
            this.groupBoxVertex.PerformLayout();
            this.groupBoxEdge.ResumeLayout(false);
            this.groupBoxEdge.PerformLayout();
            this.groupBoxPath.ResumeLayout(false);
            this.groupBoxPath.PerformLayout();
            this.groupBoxResult.ResumeLayout(false);
            this.groupBoxResult.PerformLayout();
            this.ResumeLayout(false);
        }
        
        #endregion
        
        private System.Windows.Forms.Panel graphPanel;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.GroupBox groupBoxVertex;
        private System.Windows.Forms.Button btnRemoveVertex;
        private System.Windows.Forms.Button btnAddVertex;
        private System.Windows.Forms.TextBox txtVertexId;
        private System.Windows.Forms.Label lblVertexId;
        private System.Windows.Forms.GroupBox groupBoxEdge;
        private System.Windows.Forms.Button btnRemoveEdge;
        private System.Windows.Forms.Button btnAddEdge;
        private System.Windows.Forms.TextBox txtEdgeWeight;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.TextBox txtEdgeTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtEdgeFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.GroupBox groupBoxPath;
        private System.Windows.Forms.Button btnFindPath;
        private System.Windows.Forms.ComboBox cmbDestVertex;
        private System.Windows.Forms.Label lblDestVertex;
        private System.Windows.Forms.ComboBox cmbSourceVertex;
        private System.Windows.Forms.Label lblSourceVertex;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnClearGraph;
        private System.Windows.Forms.Button btnResetDefault;
    }
}
