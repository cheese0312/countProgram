' Global 변수 설정
Imports System.IO

Module mod_Global
    Public ApplicationName As String = System.Environment.GetCommandLineArgs(0).Replace(Application.StartupPath, "").Replace("\", "").ToUpper.Replace(".EXE", "").Replace(".VSHOST", "")
    Public IniFile As String = Application.StartupPath + "\" + ApplicationName + ".ini"
    Public LogDirectory As String = Application.StartupPath & "\log"
    Public TempDirectory As String = Application.StartupPath & "\temp"

    Public Sub Common_Insert_Log(Log As String, Optional lstLog As ListBox = Nothing)
        Dim objStreamWriter As StreamWriter

        Dim LogFile As String = LogDirectory & "\" & Format(Now, "yyyyMMdd") & ".log"

        If Not System.IO.Directory.Exists(LogDirectory) Then
            System.IO.Directory.CreateDirectory(LogDirectory)
        End If
        objStreamWriter = New StreamWriter(LogFile, True)
        objStreamWriter.WriteLine("[" & Now & "]" & Log)
        objStreamWriter.Close()

        If lstLog IsNot Nothing Then
            If lstLog.Items.Count > 100 Then
                lstLog.Items.Clear()
                Application.DoEvents()
            End If
            lstLog.Items.Add("[" & Now & "] " & Log)
            lstLog.SelectedIndex = lstLog.Items.Count - 1
        End If

    End Sub

    Public Sub Common_Insert_Log(Ext As String, Log As String, Optional lstLog As ListBox = Nothing)
        Dim objStreamWriter As StreamWriter

        Dim LogFile As String = LogDirectory & "\" & Format(Now, "yyyyMMdd") & ".log"
        LogFile = LogFile.Replace(".log", "." & Ext)

        If Not System.IO.Directory.Exists(LogDirectory) Then
            System.IO.Directory.CreateDirectory(LogDirectory)
        End If
        objStreamWriter = New StreamWriter(LogFile, True)
        objStreamWriter.WriteLine("[" & Now & "]" & Log)
        objStreamWriter.Close()

        If lstLog IsNot Nothing Then
            If lstLog.Items.Count > 100 Then
                lstLog.Items.Clear()
            End If
            lstLog.Items.Add("[" & Now & "] " & Log)
            lstLog.SelectedIndex = lstLog.Items.Count - 1
        End If

    End Sub

    Public Sub Common_Insert_Log_NoTime(Ext As String, Log As String)
        Dim objStreamWriter As StreamWriter

        Dim LogFile As String = LogDirectory & "\" & Format(Now, "yyyyMMdd") & ".log"
        LogFile = LogFile.Replace(".log", "." & Ext)

        If Not System.IO.Directory.Exists(LogDirectory) Then
            System.IO.Directory.CreateDirectory(LogDirectory)
        End If
        objStreamWriter = New StreamWriter(LogFile, True)
        objStreamWriter.WriteLine(Log)
        objStreamWriter.Close()
    End Sub

    Public Function WriteFile(filename As String, txt As String) As Boolean
        Dim ReturnValue As Boolean = False

        Dim objStreamWriter As StreamWriter
        Dim TempFile As String = Application.StartupPath & "\" & filename


        If Not System.IO.Directory.Exists(LogDirectory) Then
            System.IO.Directory.CreateDirectory(LogDirectory)
        End If
        objStreamWriter = New StreamWriter(TempFile, False)
        objStreamWriter.Write(txt)
        objStreamWriter.Close()

        Return ReturnValue
    End Function

    Public Function ReadFile(filename As String) As String
        Dim objStreamReader As StreamReader = Nothing

        Dim ReturnValue As String = ""
        Try

            Dim TempFile As String = Application.StartupPath & "\" & filename


            If Not System.IO.Directory.Exists(LogDirectory) Then
                System.IO.Directory.CreateDirectory(LogDirectory)
            End If
            objStreamReader = New StreamReader(TempFile, True)
            ReturnValue = objStreamReader.ReadToEnd()
        Catch ex As Exception
            ReturnValue = ""
        Finally
            If objStreamReader IsNot Nothing Then
                objStreamReader.Close()
            End If
        End Try
        Return ReturnValue

    End Function

    ' 내 코드 -----------------------------------------------------------------------------------------------------------------------
    Public Sub Common_Count_Log(Log As String, Optional CntLog As ListBox = Nothing)
        Dim folderPath As String = "query"
        Dim selectedDate As DateTime = frmMain.dtStart.Value
        Dim formattedDate As String = selectedDate.ToString("yyyyMMdd")
        Dim filePath As String = Path.Combine(folderPath, formattedDate & ".sql")
        Dim fileContent As String = ""

        Try
            fileContent = File.ReadAllText(filePath)
        Catch ex As Exception
            Console.WriteLine("파일을 읽어오는 동안 예외가 발생했습니다: " & ex.Message)
        End Try
    End Sub

    'Function GetInsertCount(content As String) As Integer
    '    Dim insertCount As Integer = 0

    '    ' 파일 내용을 줄 단위로 분할하여 각 줄에 대해 INSERT문인지 확인
    '    Dim lines As String() = content.Split(Environment.NewLine)
    '    For Each line As String In lines
    '        If line.Trim().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) Then
    '            insertCount += 1
    '        End If
    '    Next

    '    Return insertCount
    'End Function

    'Function GetNofileCount(content As String) As Integer
    '    Dim nofileCount As Integer = 0

    '    ' 파일 내용을 줄 단위로 분할하여 파일없음인지 확인
    '    Dim lines As String() = content.Split(Environment.NewLine)
    '    For Each line As String In lines
    '        If line.Contains("파일없음") Then
    '            nofileCount += 1
    '        End If
    '    Next

    '    Return nofileCount
    'End Function

    'Function GetDuplicateCount(content As String) As Integer
    '    Dim dofileCount As Integer = 0

    '    ' 파일 내용을 줄 단위로 분할하여 중복파일인지 확인
    '    Dim lines As String() = content.Split(Environment.NewLine)
    '    For Each line As String In lines
    '        If line.Contains("중복파일") Then
    '            dofileCount += 1
    '        End If
    '    Next

    '    Return dofileCount
    'End Function

End Module
