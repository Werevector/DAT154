// Assignment3.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "Assignment3.h"
#include "Crossing.h"

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name
HWND mainWnd;

rectangle winParameters;
Crossing crossing;

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK	Spawn(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    // TODO: Place code here.

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_ASSIGNMENT3, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_ASSIGNMENT3));

    MSG msg = { 0 };
	while (msg.message != WM_QUIT) {
		// Main message loop:
		//while (GetMessage(&msg, nullptr, 0, 0))
		while (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
			{
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
		}
		crossing.update();
		InvalidateRect(mainWnd, NULL, TRUE);
		UpdateWindow(mainWnd);
		
	}

    return (int) msg.wParam;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ASSIGNMENT3));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = MAKEINTRESOURCEW(IDC_ASSIGNMENT3);
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   winParameters.x = 100;
   winParameters.y = 10;
   winParameters.w = 1000;
   winParameters.h = 1000;

   

   mainWnd = CreateWindowW(
	   szWindowClass,
	   szTitle,
	   WS_OVERLAPPEDWINDOW,
	   winParameters.x,
	   winParameters.y,
	   winParameters.w,
	   winParameters.h,
	   nullptr,
	   nullptr,
	   hInstance,
	   nullptr
	   );

   if (!mainWnd)
   {
      return FALSE;
   }

   SetTimer(mainWnd, IDT_TIMER1, 3000, (TIMERPROC)NULL);
   ShowWindow(mainWnd, nCmdShow);
   UpdateWindow(mainWnd);

   return TRUE;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
	case WM_CREATE:
	{
		RECT area;
		GetClientRect(hWnd, &area);

		rectangle drawingarea;
		drawingarea.x = area.left;
		drawingarea.y = area.top;
		drawingarea.w = area.right;
		drawingarea.h = area.bottom;

		crossing.init(hInst, drawingarea);
		SetTimer(mainWnd, IDT_TIMER1, 1000, (TIMERPROC)NULL);

		//HWND hwndButton = CreateWindow(
		//	L"BUTTON",  // Predefined class; Unicode assumed 
		//	L"OK",      // Button text 
		//	WS_TABSTOP | WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,  // Styles 
		//	10,         // x position 
		//	10,         // y position 
		//	50,        // Button width
		//	50,        // Button height
		//	hWnd,     // Parent window
		//	NULL,       // No menu.
		//	(HINSTANCE)GetWindowLong(hWnd, IDM_SPAWN),
		//	NULL);      // Pointer not needed.

	}
    case WM_COMMAND:
        {
            int wmId = LOWORD(wParam);
            // Parse the menu selections:
            switch (wmId)
            {
            case IDM_ABOUT:
                DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
                break;
			case IDM_SPAWN:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_SPAWN), hWnd, Spawn);
				break;
            case IDM_EXIT:
                DestroyWindow(hWnd);
                break;
				
            default:
                return DefWindowProc(hWnd, message, wParam, lParam);
            }
        }
        break;
	case WM_KEYDOWN:
		switch (wParam) {
		case VK_LEFT:
			crossing.pwDOWN();
			break;
		case VK_RIGHT:
			crossing.pwUP();
			break;
		case VK_UP:
			crossing.pnUP();
			break;
		case VK_DOWN:
			crossing.pnDOWN();
			break;
		}
		break;
    case WM_PAINT:
        {
			PAINTSTRUCT ps;
			HDC hdc = BeginPaint(hWnd, &ps);
			RECT rect;
			GetClientRect(hWnd, &rect);
			int width = rect.right;
			int height = rect.bottom;

			HDC backbufferDC = CreateCompatibleDC(hdc);
			HBITMAP backbuffer = CreateCompatibleBitmap(hdc, width, height);

			int savedDC = SaveDC(backbufferDC);
			SelectObject(backbufferDC, backbuffer);
			HBRUSH hBrush = CreateSolidBrush(RGB(255, 255, 255));
			FillRect(backbufferDC, &rect, hBrush);
			DeleteObject(hBrush);

			crossing.draw(backbufferDC);

			BitBlt(hdc, 0, 0, width, height, backbufferDC, 0, 0, SRCCOPY);
			RestoreDC(backbufferDC, savedDC);

			DeleteObject(backbuffer);
			DeleteDC(backbufferDC);

			EndPaint(hWnd, &ps);
        }
        break;
	case WM_TIMER:
		crossing.tick();
		break;
	case WM_ERASEBKGND:
		return 1;
	case WM_SIZING:
	{
		//DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
		RECT area;
		GetClientRect(hWnd, &area);

		rectangle drawingarea;
		drawingarea.x = area.left;
		drawingarea.y = area.top;
		drawingarea.w = area.right;
		drawingarea.h = area.bottom;
		crossing.resize(drawingarea);
	}
    case WM_DESTROY:
		KillTimer(hWnd, IDT_TIMER1);
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
    }
    return 0;
}

INT_PTR CALLBACK Spawn(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	UNREFERENCED_PARAMETER(lParam);

	int pw;
	int pn;

	switch (message)
	{
	case WM_INITDIALOG:
		return (INT_PTR)TRUE;

	case WM_HSCROLL:
		
	case WM_COMMAND:
		switch (wParam) {
		case IDOK:
			pn = GetDlgItemInt(hWnd, IDC_EDITPN, NULL, FALSE);
			pw = GetDlgItemInt(hWnd, IDC_EDITPW, NULL, FALSE);
			
			crossing.setSpawnRate(pw, pn);

			EndDialog(hWnd, LOWORD(wParam));
			return (INT_PTR)TRUE;
		case IDCANCEL:
			EndDialog(hWnd, LOWORD(wParam));
			return (INT_PTR)TRUE;

		}
		break;
	}
	return (INT_PTR)FALSE;
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    UNREFERENCED_PARAMETER(lParam);
    switch (message)
    {
    case WM_INITDIALOG:
        return (INT_PTR)TRUE;

    case WM_COMMAND:
        if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
        {
            EndDialog(hDlg, LOWORD(wParam));
            return (INT_PTR)TRUE;
        }
        break;
    }
    return (INT_PTR)FALSE;
}
