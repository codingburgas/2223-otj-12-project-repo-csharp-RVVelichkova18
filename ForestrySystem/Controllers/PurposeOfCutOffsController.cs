using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestrySystem.Data;
using ForestrySystem.Models;
using Microsoft.AspNetCore.Authorization;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Filters;
using System.Data;
using ForestrySystem.Services;
using ForestrySystem.Enums;
using Accord.Collections;

namespace ForestrySystem.Controllers
{

    public class PurposeOfCutOffsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PurposeOfCutOffsService _purposeOfCutOffsService;

        public PurposeOfCutOffsController(ApplicationDbContext context, PurposeOfCutOffsService purposeOfCutOffsService)
        {
            _context = context;
            _purposeOfCutOffsService = purposeOfCutOffsService;

        }

        // GET: PurposeOfCutOffs
        public async Task<IActionResult> Index(string SearchString)
        {
            //return View(await _context.Institutions.ToListAsync());
            ViewData["CurrentFilter"] = SearchString;
            IQueryable<PurposeOfCutOff> purposes = _purposeOfCutOffsService.GetPurposeOfCutOffs();
            if (!String.IsNullOrEmpty(SearchString))
            {
                purposes = _purposeOfCutOffsService.GetFilteredPurposeOfCutOffs(SearchString, purposes);
            }
            return View(purposes);
        }


        // GET: PurposeOfCutOffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurposeOfCutOff == null)
            {
                return NotFound();
            }

            PurposeOfCutOff? purposeOfCutOff = await _purposeOfCutOffsService.GetPurposeOfCurOff(id);
            if (purposeOfCutOff == null)
            {
                return NotFound();
            }

            return View(purposeOfCutOff);
        }



        // GET: PurposeOfCutOffs/Create
        [Authorize(Roles = "Expert,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurposeOfCutOffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Purpose,PercentagePerYear")] PurposeOfCutOff purposeOfCutOff)
        {
            if (ModelState.IsValid)
            {
                await _purposeOfCutOffsService.CreatePurposeOdCurOff(purposeOfCutOff);
                return RedirectToAction(nameof(Index));
            }
            return View(purposeOfCutOff);
        }



        // GET: PurposeOfCutOffs/Edit/5
        [Authorize(Roles = "Expert,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurposeOfCutOff == null)
            {
                return NotFound();
            }

            var purposeOfCutOff = await _purposeOfCutOffsService.GetPurposeOfCurOff(id);
            if (purposeOfCutOff == null)
            {
                return NotFound();
            }
            return View(purposeOfCutOff);
        }

        // POST: PurposeOfCutOffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Purpose,PercentagePerYear")] PurposeOfCutOff purposeOfCutOff)
        {
            if (id != purposeOfCutOff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _purposeOfCutOffsService.UpdatePurposeOfCutOff(purposeOfCutOff);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_purposeOfCutOffsService.PurposeOfCutOffExists(purposeOfCutOff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(purposeOfCutOff);
        }



        // GET: PurposeOfCutOffs/Delete/5
        [Authorize(Roles = "Expert,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurposeOfCutOff == null)
            {
                return NotFound();
            }

            var purposeOfCutOff = await _purposeOfCutOffsService.GetPurposeOfCurOff(id);
            if (purposeOfCutOff == null)
            {
                return NotFound();
            }

            return View(purposeOfCutOff);
        }

        // POST: PurposeOfCutOffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurposeOfCutOff == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PurposeOfCutOff'  is null.");
            }
            await _purposeOfCutOffsService.RemovePurposeOfCutOff(id);
            return RedirectToAction(nameof(Index));
        }

        //public class DecisionTree<T>
        //{
        //    public void Train(Dictionary<string, double[]> data, string[] labels)
        //    {
        //        if (data.Count != labels.Length)
        //        {
        //            throw new ArgumentException("The number of data points and labels do not match.");
        //        }

        //        // Convert the data dictionary into a double array
        //        double[][] dataPoints = new double[data.Count][];
        //        int index = 0;
        //        foreach (var kvp in data)
        //        {
        //            dataPoints[index] = kvp.Value;
        //            index++;
        //        }

        //        // Train the decision tree
        //        tree.Learn(dataPoints, labels);
        //    }

        //}
        //public Dictionary<string, double> DistributeDataIntoCategories(Dictionary<string, double> data)
        //{
        //    // create decision tree
        //    DecisionTree<string> tree = new DecisionTree<string>()
        //    {
        //        Root = new DecisionNode<string>()
        //        {
        //            SplitFunction = (dict) =>
        //            {
        //                double totalPercentage = dict.Values.Sum();

        //                if (dict[PurposeOfCutOffsEnum.Factories.ToString()] / totalPercentage > 0.5)
        //                {
        //                    return PurposeOfCutOffsEnum.Factories.ToString();
        //                }
        //                else if (dict[PurposeOfCutOffsEnum.Civils.ToString()] / totalPercentage > 0.2)
        //                {
        //                    return PurposeOfCutOffsEnum.Civils.ToString();
        //                }
        //                else
        //                {
        //                    return PurposeOfCutOffsEnum.Export.ToString();
        //                }
        //            }
        //        }
        //    };

        //    // train decision tree with data
        //    foreach (var kvp in data)
        //    {
        //        Dictionary<string, double> dict = new Dictionary<string, double>()
        //{
        //    { PurposeOfCutOffsEnum.Factories.ToString(), 0 },
        //    { PurposeOfCutOffsEnum.Civils.ToString(), 0 },
        //    { PurposeOfCutOffsEnum.Export.ToString(), 0 }
        //};
        //        dict[kvp.Key] = kvp.Value;
        //        tree.Train(dict);
        //    }

        //    // predict with decision tree
        //    var result = new Dictionary<string, double>();
        //    foreach (var kvp in data)
        //    {
        //        Dictionary<string, double> dict = new Dictionary<string, double>()
        //{
        //    { PurposeOfCutOffsEnum.Factories.ToString(), 0 },
        //    { PurposeOfCutOffsEnum.Civils.ToString(), 0 },
        //    { PurposeOfCutOffsEnum.Export.ToString(), 0 }
        //};
        //        dict[kvp.Key] = kvp.Value;
        //        string category = tree.Predict(dict);
        //        if (!result.ContainsKey(category))
        //        {
        //            result.Add(category, 0);
        //        }
        //        result[category] += kvp.Value;
        //    }

        //    // normalize result
        //    double totalPercentage = result.Values.Sum();
        //    foreach (var kvp in result.ToList())
        //    {
        //        result[kvp.Key] = kvp.Value / totalPercentage;
        //    }

        //    return result;
        //}

        //public class DecisionTree<T>
        //{
        //    public DecisionNode<T> root;

        //    public void AddRoot(T value)
        //    {
        //        root = new DecisionNode<T>(value);
        //    }

        //    public DecisionNode<T> AddChild(DecisionNode<T> parent, T value, double weight)
        //    {
        //        DecisionNode<T> node = new DecisionNode<T>(value, weight);
        //        parent.Children.Add(value.ToString(), node);
        //        return node;
        //    }

        //    public void Train(Dictionary<string, double>[] trainingData)
        //    {
        //        foreach (Dictionary<string, double> data in trainingData)
        //        {
        //            DecisionNode<T> node = root;
        //            while (node.Children.Count > 0)
        //            {
        //                // Get the next feature to evaluate
        //                string feature = node.Decide(data);
        //                // Find the child node that matches the feature value
        //                DecisionNode<T> child = node.GetChild(feature);
        //                // Move down to the child node
        //                node = child;
        //            }
        //            // Set the result on the leaf node
        //            node.Result = data["result"];
        //        }
        //    }

        //    public double Predict(Dictionary<string, double> inputData)
        //    {
        //        DecisionNode<T> node = root;
        //        while (node.Children.Count > 0)
        //        {
        //            // Get the next feature to evaluate
        //            string feature = node.Decide(inputData);
        //            // Find the child node that matches the feature value
        //            DecisionNode<T> child = node.GetChild(feature);
        //            // Move down to the child node
        //            node = child;
        //        }
        //        // Return the result on the leaf node
        //        return node.Result;
        //    }
        //}

        //public class DecisionNode<T>
        //{
        //    public T Value { get; set; }
        //    public double Weight { get; set; }
        //    public Dictionary<string, DecisionNode<T>> Children { get; set; }
        //    public double Result { get; set; }
        //    public Func<Dictionary<string, double>, string> decideFunction;

        //    public DecisionNode(T value, double weight = 1.0)
        //    {
        //        Value = value;
        //        Weight = weight;
        //        Children = new Dictionary<string, DecisionNode<T>>();
        //    }

        //    public void AddChild(string featureValue, DecisionNode<T> child)
        //    {
        //        Children[featureValue] = child;
        //    }

        //    public DecisionNode<T> GetChild(string featureValue)
        //    {
        //        return Children[featureValue];
        //    }

        //    public string Decide(Dictionary<string, double> inputData)
        //    {
        //        return decideFunction(inputData);
        //    }

        //    public void SetDecideFunction(Func<Dictionary<string, double>, string> decideFunction)
        //    {
        //        this.decideFunction = decideFunction;
        //    }
        //}

        //public static class DecisionTreeHelper
        //{
        //    //private static DecisionTree<double> CreateDecisionTree()
        //    //{
        //    //    var tree = new DecisionTree<double>();
        //    //    tree.AddRoot("Purpose of CutOffs");

        //    //    var factoriesNode = tree.AddChild(tree.root, "Factories", 0.5);
        //    //    factoriesNode.AddChild("Heavy Industry", new DecisionNode<double>(0.7, "Heavy Industry"));
        //    //    factoriesNode.AddChild("Light Industry", new DecisionNode<double>(0.3, "Light Industry"));

        //    //    var civilsNode = tree.AddChild(tree.root, "Civils", 0.2);
        //    //    civilsNode.AddChild("Roads", new DecisionNode<double>(0.4, "Roads"));
        //    //    civilsNode.AddChild("Buildings", new DecisionNode<double>(0.6, "Buildings"));

        //    //    var exportNode = tree.AddChild(tree.root, "Export", 0.3);
        //    //    exportNode.AddChild("Goods", new DecisionNode<double>(0.5, "Goods"));
        //    //    exportNode.AddChild("Materials", new DecisionNode<double>(0.8, "Materials"));

        //    //    // Set the decide functions for the nodes
        //    //    factoriesNode.SetDecideFunction(data => data["industry"] == "heavy" ? 0.7 : 0.3);
        //    //    civilsNode.SetDecideFunction(data => data["project"] == "roads" ? 0.4 : 0.6);
        //    //    exportNode.SetDecideFunction(data => data["type"] == "materials" ? 0.8 : 0.5);

        //    //    return tree;
        //    //}



        //    public static void ExampleUsage()
        //    {
        //        // Create the decision tree
        //        var tree = CreateDecisionTree();

        //        // Define some example input data
        //        var inputData = new Dictionary<string, double>()
        //        {
        //             { "industry", 1.0 },
        //             { "project", 0.0 },
        //             { "type", 1.0 }
        //        };

        //        // Use the decision tree to make a prediction
        //        var result = tree.Predict(inputData);

        //        Console.WriteLine($"Prediction: {result}");
        //    }
        //}

    }

}
