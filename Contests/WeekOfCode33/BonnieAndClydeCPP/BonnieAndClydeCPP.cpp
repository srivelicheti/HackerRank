// BonnieAndClydeCPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
// BonnieAndClydeCPP.cpp : Defines the entry point for the console application.
//

#include <fstream>
#include <iostream>
#include <vector>
#include <unordered_set>
#include <unordered_map>

using namespace std;

int FindPath(int s, int d, vector<vector<int>>& graph, unordered_set<int>* path, vector<bool>& visited)
{
	if (s == d) {
		path->insert(s);
		return s;
	}

	//if (graph[s] != nullptr)
	{
		for (auto i : graph[s])
		{
			if (!visited[i])
			{
				visited[i] = true;
				auto found = FindPath(i, d, graph, path, visited);
				if (found >= 0)
				{
					path->insert(s);
					return s;
				}
			}
		}
	}
	return -1;
}

int FindPath2(int s, int d, vector<vector<int>>& graph, unordered_set<int>* path, int n, int u, int v, int w)
{
	vector<bool> visited(n + 1);
	visited[s] = true;
	return FindPath(s, d, graph, path, visited);
}

int FindOtherPath(int s, int d, vector<vector<int>>& graph, unordered_set<int>* path, vector<bool>& visited, unordered_set<int>* existingPath)
{
	if (existingPath->find(s) != existingPath->end())
		return -1;

	if (s == d) {
		path->insert(s);
		return s;
	}

	//if (graph[s] != nullptr)
	{
		for (auto i : graph[s])
		{
			if (!visited[i])
			{
				visited[i] = true;
				if (existingPath->find(i) == existingPath->end()) {
					auto found = FindOtherPath(i, d, graph, path, visited, existingPath);
					if (found >= 0)
					{
						path->insert(s);
						return s;
					}
				}
			}
		}
	}
	return -1;
}

int FindOtherPath2(int s, int d, vector<vector<int>>& graph, unordered_set<int>* path, unordered_set<int>* existingPath, int n)
{
	vector<bool> visited(n+1);
	visited[s] = true;
	return FindOtherPath(s, d, graph, path, visited, existingPath);
}

bool AreDisoint(unordered_set<int> s1, unordered_set<int> s2)
{
	if (s1.empty() || s2.empty())
		return true;

	for (auto i : s1)
	{
		if (s2.find(i) != s2.end())
			return false;
	}
	return true;
}


void SolveOne(vector<vector<int>>& graph, int n)
{
	int u, v, w;
	cin >> u >> v >> w;

	unordered_map<int, vector<unordered_set<int>>> uPaths;
	unordered_map<int, vector<unordered_set<int>>> vPaths;

//	vector<bool> visited(n + 1);
	//fill(visited.begin(), visited.end(), false);

	if ((u != w && v != w) && (graph[w].size() == 1))
	{
		cout << "NO" << endl;
		return;
	}
	if (u == w && v == w) {
		cout << "YES" << endl;
		return;
	}

	vector<int> towPathsFoundU;
	vector<int> twoPathsFoundV;

	{
		for (auto src : graph[w])
		{
			if (src != v && u != w)
			{

				unordered_set<int> path;
				//visited[src] = true;
				//visited[w] = true;
				auto p1 = FindPath2(src, u, graph, &path, n, u ,v, w);
				if (p1 >= 0)
				{
					vector<unordered_set<int>> ins;
					ins.push_back(path);
					uPaths.insert({ src,ins });

					unordered_set<int> path2;
					int p2 = FindOtherPath2(src, u, graph, &path2, &path, n);
					if (p2 >= 0) {
						towPathsFoundU.push_back(src);
						if (twoPathsFoundV.size()>1)
						{
							cout << "YES" << endl;
							return;
						}
						uPaths.find(src)->second.push_back(path2);
					}
				}

			}
			if (v != w && src != u)
			{
				
				unordered_set<int> path;
				auto p1 = FindPath2(src, v, graph, &path, n,u, v, w);
				if (p1 >= 0)
				{
					vector<unordered_set<int>> ins;
					ins.push_back(path);
					vPaths.insert({ src, ins });

					unordered_set<int> path2;
					auto p2 = FindOtherPath2(src, v, graph, &path2, &path, n );
					if (p2 >= 0) {
						twoPathsFoundV.push_back(src);
						if (towPathsFoundU.size() > 1)
						{
							cout << "YES" << endl;
							return;
						}
						vPaths.find(src)->second.push_back(path2);

					}
				}
			}

		}
	}


	if ((u == w && vPaths.size() > 0) || (v == w && uPaths.size()>0) || (u == w && v == w))
	{
		cout << "YES" << endl;
		return;
	}

	for (auto u_path : uPaths)
	{
		if (u_path.second.size() > 0)
		{
			for (auto p1 : u_path.second)
			{
				for (auto v_path : vPaths)
				{
					if (v_path.first != u_path.first && v_path.second.size() > 0)
					{
						for (auto path : v_path.second)
						{
							if (AreDisoint(path, p1))
							{
								cout << "YES" << endl;
								return;
							}
						}
					}
				}
			}

		}
	}
	cout << "NO" << endl;
}

int main()
{
	ios::sync_with_stdio(false);
	cin.tie(NULL);

#if _DEBUG
	std::ifstream ins("input.txt");
	std::streambuf *cinbuf = std::cin.rdbuf(); //save old buf
	std::cin.rdbuf(ins.rdbuf()); //redirect std::cin to in.txt!
#endif

	int n, m, q;
	cin >> n >> m >> q;

	vector<vector<int>> graph(n + 1);

	while (m--)
	{
		int s, e;
		cin >> s >> e;
		graph[s].push_back(e);
		graph[e].push_back(s);
	}

	while (q--) {

		SolveOne(graph, n);
	}

	return 0;
}



