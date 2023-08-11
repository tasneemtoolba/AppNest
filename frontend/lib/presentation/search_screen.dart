import 'package:appnest/api.dart';
import 'package:appnest/domain/score_model.dart';
import 'package:flutter/material.dart';

class SearchScreen extends StatefulWidget {
  const SearchScreen({Key? key}) : super(key: key);

  @override
  State<SearchScreen> createState() => _SearchScreenState();
}

class _SearchScreenState extends State<SearchScreen> {
  List<ScoreModel> scores = [];
  late String searchQuery = '';

  //Main Widget
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Score Screen')),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: TextField(
              onChanged: (value) {
                setState(() {
                  searchQuery = value;
                });
              },
              decoration: const InputDecoration(
                labelText: 'Search',
                hintText: 'Enter search query',
                prefixIcon: Icon(Icons.search),
              ),
            ),
          ),
          Expanded(
            child: FutureBuilder<List<ScoreModel>>(
              future: API().getScores(searchQuery, null),
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return const Center(child: CircularProgressIndicator());
                } else if (searchQuery.length != 0 && snapshot.hasError) {
                  return Text('Error: ${snapshot.error}');
                } else if (searchQuery.length == 0 ||
                    !snapshot.hasData ||
                    snapshot.data!.isEmpty) {
                  return const Text('No scores found.');
                } else {
                  return ListView.builder(
                    itemCount: snapshot.data!.length,
                    itemBuilder: (context, index) {
                      ScoreModel score = snapshot.data![index];
                      return ListTile(
                        title: Text(score.productName),
                        subtitle: Text('Score: ${score.score.toString()}'),
                      );
                    },
                  );
                }
              },
            ),
          ),
        ],
      ),
    );
  }
}
