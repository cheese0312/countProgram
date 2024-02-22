' Global 변수 설정
Imports System.IO

Module mod_Global

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

    Function GetInsertCount(content As String) As Integer
        Dim insertCount As Integer = 0

        ' 파일 내용을 줄 단위로 분할하여 각 줄에 대해 INSERT문인지 확인
        Dim lines As String() = content.Split(Environment.NewLine)
        For Each line As String In lines
            If line.Trim().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase) Then
                insertCount += 1
            End If
        Next

        Return insertCount
    End Function

    Function GetNofileCount(content As String) As Integer
        Dim nofileCount As Integer = 0

        ' 파일 내용을 줄 단위로 분할하여 파일없음인지 확인
        Dim lines As String() = content.Split(Environment.NewLine)
        For Each line As String In lines
            If line.Contains("파일없음") Then
                nofileCount += 1
            End If
        Next

        Return nofileCount
    End Function

    Function GetDuplicateCount(content As String) As Integer
        Dim dofileCount As Integer = 0

        ' 파일 내용을 줄 단위로 분할하여 중복파일인지 확인
        Dim lines As String() = content.Split(Environment.NewLine)
        For Each line As String In lines
            If line.Contains("중복파일") Then
                dofileCount += 1
            End If
        Next

        Return dofileCount
    End Function

End Module
