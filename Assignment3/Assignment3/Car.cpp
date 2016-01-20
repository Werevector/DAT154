#include "stdafx.h"
#include "Car.h"


Car::Car(int x, int y, Direction dir)
{
	position.x = x;
	position.y = y;

	velocity.x = 0;
	velocity.y = 0;

	acceleration = 2;

	direction = dir;

}


Car::~Car()
{
	DeleteObject(bitMap.image);
}

void Car::draw(HDC drawcanvas)
{
	int nx = position.x - (bitMap.width / 2);
	int ny = position.y - (bitMap.height / 2);

	HDC carDC = CreateCompatibleDC(drawcanvas);

	SelectObject(carDC, bitMap.image);
	
	HDC stretchDC = CreateCompatibleDC(drawcanvas);
	//StretchBlt(stretchDC, nx, ny, 400, 400, carDC, nx,ny,600,200, SRCCOPY);
	BitBlt(drawcanvas, nx, ny, bitMap.width, bitMap.height, carDC, 0, 0, SRCCOPY);

	DeleteObject(carDC);

}

bool Car::loadBitMap(HINSTANCE hInst, int IDB, int w, int h)
{
	bool result = true;
	bitMap.image = LoadBitmap(hInst, MAKEINTRESOURCE(IDB));
	bitMap.width = w;
	bitMap.height = h;

	//carDC = CreateCompatibleDC(originDC);



	return result;
}
