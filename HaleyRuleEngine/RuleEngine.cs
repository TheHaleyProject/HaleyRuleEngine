﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using Haley.Abstractions;
using Haley.Enums;
using Haley.Models;

namespace Haley.RuleEngine
{
    public sealed class RuleEngine
    {
        #region Static Methods
        public static Dictionary<Rule, ExpressionBlock<T>> CompileRules<T>(List<Rule> rule_list)
        {
            Dictionary<Rule, ExpressionBlock<T>> result = new Dictionary<Rule, ExpressionBlock<T>>();

            foreach (Rule rule in rule_list)
            {
                var _expBlockForRule = CompileBlocks<T>(rule.block);
                result.Add(rule, _expBlockForRule);
            }
            return result;
        }

        private static ExpressionBlock<T> CompileBlocks<T>(RuleBlock _ruleBlock)
        {
            //Process all expressions inside the block
            List<ExpressionBase<T>> _exp_list = new List<ExpressionBase<T>>();
            foreach (var _axiom in _ruleBlock.getAxioms())
            {
                ExpressionMaker exp_maker = new ExpressionMaker(_axiom);
                ExpressionBase<T> _exp_inst = new ExpressionBase<T>(_axiom.id, exp_maker.Make);
                _exp_list.Add(_exp_inst);
            }

            //Process all blocks inside the block
            List<ExpressionBlock<T>> _block_list = new List<ExpressionBlock<T>>();
            foreach (var sub_block in _ruleBlock.getBlocks())
            {
                _block_list.Add(CompileBlocks<T>(sub_block));
            }

            //TODO: FIX PARAMETER ARGUMENTS FOR THE EXPRESSION BLOCK
            ExpressionBlock<T> _expblock = new ExpressionBlock<T>(_ruleBlock.getOperator(), _exp_list, _block_list);
            return _expblock;
        }
        #endregion
    }

    public sealed class RuleEngine<T>
    {
        private Dictionary<Rule, ExpressionBlock<T>> rule_dic = new Dictionary<Rule, ExpressionBlock<T>>();

        private Dictionary<Rule, List<T>> _passed_items = new Dictionary<Rule, List<T>>();
        private Dictionary<Rule, List<T>> _all_items = new Dictionary<Rule, List<T>>();

        public void ProcessRules(T target)
        {
            foreach (var item in rule_dic)
            {
                //Each rule should be added to the dictionary
                if (!_passed_items.ContainsKey(item.Key)) _passed_items.Add(item.Key, new List<T>());
                if (!_all_items.ContainsKey(item.Key)) _all_items.Add(item.Key, new List<T>());
                item.Value.validate(target);
                var _status = item.Value.getBlockStatus();
                item.Key.status = _status;
                if (_status == ActionStatus.Pass)
                {
                    _passed_items[item.Key].Add(target);
                }
                _all_items[item.Key].Add(target); //Add all items
            }
        }

        public void Compile(List<Rule> rules)
        {
            rule_dic = RuleEngine.CompileRules<T>(rules);
        }

        public Dictionary<Rule, List<T>> getResults(bool only_passed_items = true)
        {
            if (only_passed_items) return _passed_items;
            return _all_items;
        }

        public void clearResults()
        {
            _passed_items = new Dictionary<Rule, List<T>>();
            _all_items = new Dictionary<Rule, List<T>>();
        }

        public Dictionary<Rule, ExpressionBlock<T>> getRules()
        {
            return rule_dic;
        }
        public RuleEngine(){}
    }
}
