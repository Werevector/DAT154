#pragma once
#include <string>
#include <iostream>
#include <fstream>
#include <random>

using namespace std;

static string rSize[] = {"SMALL", "MEDIUM", "LARGE", "KING"};

static string cost[] = { "LOW", "MEDIUM", "HIGH" };

bool generateSQL(string fileName, int number) {
	
	bool success = true;

	ofstream outputFile;
	outputFile.open(fileName);

	int bednr;
	int c;

	std::random_device rd;
	std::mt19937 rng(rd());
	std::uniform_int_distribution<int> bedsG(1, 4);
	std::uniform_int_distribution<int> priceG(1, 3);


	outputFile << "insert into rooms (bedsNr, roomSize, pricing) values\n";

	for (int i = 0; i < number; i++) {
		bednr = bedsG(rng);
		c = priceG(rng);
	
		outputFile << "(" << bednr << ", '" << rSize[bednr-1] << "', '" << cost[c-1] << "')" << ((i <number-1) ? ",\n":"");


	}

	outputFile << ";";

	return success;
}

int main(int argc, const char* argv[]) {
	
	cout << "MySQL insert statement generator\n made JUST for DAT154 tables\n this thing is pretty funny i guess\n";
	cout << "---------------------------------------------------\n";
	
	int rooms = 0;
	string fileName;
	
	cout << "Name the file: ";
	cin >> fileName;
	cout << '\n';

	cout << "Number of rooms: ";
	cin.ignore();
	cin >> rooms;

	generateSQL(fileName, rooms);


	return 0;

}