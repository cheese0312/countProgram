<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtStart = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtEnd = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cntLog = New System.Windows.Forms.ListBox()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "일자 : "
        '
        'dtStart
        '
        Me.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtStart.Location = New System.Drawing.Point(55, 14)
        Me.dtStart.Name = "dtStart"
        Me.dtStart.Size = New System.Drawing.Size(110, 21)
        Me.dtStart.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(171, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 12)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "~"
        '
        'dtEnd
        '
        Me.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtEnd.Location = New System.Drawing.Point(191, 14)
        Me.dtEnd.Name = "dtEnd"
        Me.dtEnd.Size = New System.Drawing.Size(112, 21)
        Me.dtEnd.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(707, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 26)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "실행"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cntLog
        '
        Me.cntLog.FormattingEnabled = True
        Me.cntLog.ItemHeight = 12
        Me.cntLog.Location = New System.Drawing.Point(12, 47)
        Me.cntLog.Name = "cntLog"
        Me.cntLog.Size = New System.Drawing.Size(775, 388)
        Me.cntLog.TabIndex = 13
        '
        'btnManual
        '
        Me.btnManual.Location = New System.Drawing.Point(620, 11)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(81, 26)
        Me.btnManual.TabIndex = 14
        Me.btnManual.Text = "실행"
        Me.btnManual.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnManual)
        Me.Controls.Add(Me.cntLog)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dtStart)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtEnd)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmMain"
        Me.Text = "LogCount"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tmrStart As Timer
    Friend WithEvents cntLog As ListBox
    Friend WithEvents Button1 As Button
    Friend WithEvents dtStart As DateTimePicker
    Friend WithEvents dtEnd As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnManual As Button
End Class
