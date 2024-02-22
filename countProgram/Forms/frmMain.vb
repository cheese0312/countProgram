Imports System.ComponentModel
Imports System.IO

Public Class frmMain
    Public Delegate Sub Count_Log_Callback(ByVal Log As String)

    Private Sub ProcessStart()
        Process()
    End Sub

    Private Sub Count_Log(Log As String)
        If Me.cntLog.InvokeRequired Then
            Dim d As New Count_Log_Callback(AddressOf Count_Log)
            Me.Invoke(d, New Object() {Log})
        Else
            If cntLog IsNot Nothing Then
                cntLog.Items.Add(Log)
            End If
            Common_Count_Log(Log, cntLog)
        End If
    End Sub

    Private Sub Process()
        Try
            Dim startDate As DateTime = dtStart.Value
            Dim endDate As DateTime = dtEnd.Value
            Dim formattedDate As String = startDate.ToString("yyyyMMdd")
            Dim forendDate As String = endDate.ToString("yyyyMMdd")

            Dim currentDate As DateTime = startDate

            Dim folderPath As String = "query" ' 폴더 경로 | D드라이브를 디폴트로 잡고 프로그램 실행 및 파일 생성

            Dim totalInsertCount As Integer = 0 ' 쿼리 생성 성공 cnt
            Dim totalNofileCount As Integer = 0 ' 파일없음 cnt
            Dim totalDuplicateCount As Integer = 0 ' 중복파일 cnt

            While currentDate <= endDate
                Dim logFilePath As String = "D:\log\" & currentDate.ToString("yyyyMMdd") & ".txt" ' 로그파일


                Dim files As String() = Directory.GetFiles(folderPath, formattedDate & ".sql") ' 성공 파일
                Dim files2 As String() = Directory.GetFiles(folderPath, formattedDate & ".err") ' 에러 파일

                ' 파일이 없는 경우 생성 X
                If files.Length = 0 AndAlso files2.Length = 0 Then
                    Exit While
                End If

                ' 생성 성공
                For Each t As String In files
                    Dim fileName As String = Path.GetFileName(t)
                    Dim fileContent As String = File.ReadAllText(t)
                    Dim insertCount As Integer = GetInsertCount(fileContent)

                    totalInsertCount += insertCount
                Next

                ' 파일없음 또는 중복파일
                For Each t As String In files2
                    Dim fileName As String = Path.GetFileName(t)
                    Dim fileContent As String = File.ReadAllText(t)
                    Dim nofileCount As Integer = GetNofileCount(fileContent)
                    Dim duplicateCount As Integer = GetDuplicateCount(fileContent)

                    totalNofileCount += nofileCount
                    totalDuplicateCount += duplicateCount
                Next

                File.WriteAllText(logFilePath, "총 INSERT문 개수 : " & totalInsertCount & " " & " " & "중복파일 개수 : " & totalDuplicateCount & "파일없음 개수 : " & totalNofileCount)

                Console.WriteLine(logFilePath & "로그 파일 생성 완료")
                Count_Log(logFilePath & " 파일 생성 완료" & logFilePath & "  총 INSERT문 개수 : " & totalInsertCount & " " & " " & "중복파일 개수 : " & totalDuplicateCount & "파일없음 개수 : " & totalNofileCount)

                ' 데이터 초기화
                totalInsertCount = 0
                totalNofileCount = 0
                totalDuplicateCount = 0

                currentDate = currentDate.AddDays(1)
                formattedDate = currentDate.ToString("yyyyMMdd")

            End While
            Console.ReadLine()

            Count_Log("파일 생성 종료")

        Catch ex As Exception
            Console.WriteLine("파일을 읽어오는 동안 예외가 발생했습니다: " & ex.Message)
        End Try
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ProcessStart()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cntLog.SelectedIndexChanged
    End Sub


End Class