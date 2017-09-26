<?php
/**
 * ShopVoteOption数据模型
 */
class ShopVoteOptionTable {
	// 数据表模型配置
	public $config = [
			'name' => 'shop_vote_option',
			'title' => '投票选项表',
			'search_key' => 'truename:请输入姓名',
			'add_button' => 1,
			'del_button' => 1,
			'search_button' => 1,
			'check_all' => 1,
			'list_row' => 10,
			'addon' => 'Vote'
	];
	
	// 列表定义
	public $list_grid = [
			'truename' => [
					'title' => '参赛者',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'truename',
					'function' => '',
					'href' => [ ]
			],
			'image' => [
					'title' => '参赛图片',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'image',
					'function' => '',
					'href' => [ ]
			],
			'manifesto' => [
					'title' => '参赛宣言',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'manifesto',
					'function' => '',
					'href' => [ ]
			],
			'introduce' => [
					'title' => '选手介绍',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'introduce',
					'function' => '',
					'href' => [ ]
			],
			'opt_count' => [
					'title' => '得票数',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'opt_count',
					'function' => '',
					'href' => [ ]
			],
			'phone' => [
					'title' => '电话',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'phone',
					'function' => '',
					'href' => [ ]
			],
			'option_status' => [
					'title' => '审核状态',
					'come_from' => 0,
					'width' => '',
					'is_sort' => 0,
					'name' => 'option_status',
					'function' => '',
					'href' => [ ]
			],
			'ids' => [
					'title' => '操作',
					'come_from' => 1,
					'width' => '',
					'is_sort' => 0,
					'href' => [
							'0' => [
									'title' => '编辑',
									'url' => 'option_edit&id=[id]'
							],
							'1' => [
									'title' => '审核',
									'url' => 'option_validate&id=[id]&status=[option_status]'
							],
							'2' => [
									'title' => '删除',
									'url' => 'option_del&id=[id]'
							],
							'3' => [
									'title' => '投票记录',
									'url' => 'show_log&option_id=[id]'
							]
					],
					'name' => 'ids',
					'function' => ''
			]
	];
	
	// 字段定义
	public $fields = [
			'truename' => [
					'title' => '参赛者',
					'field' => 'varchar(255) NULL',
					'type' => 'string',
					'is_show' => 1,
					'placeholder' => '请输入内容'
			],
			'image' => [
					'title' => '参赛图片',
					'field' => 'int(10) unsigned NULL',
					'type' => 'picture',
					'is_show' => 1,
					'placeholder' => '请输入内容'
			],
			'uid' => [
					'title' => '用户id',
					'field' => 'int(10) NULL',
					'type' => 'num',
					'placeholder' => '请输入内容'
			],
			'manifesto' => [
					'title' => '参赛宣言',
					'field' => 'text NULL',
					'type' => 'textarea',
					'is_show' => 1,
					'placeholder' => '请输入内容'
			],
			'introduce' => [
					'title' => '选手介绍',
					'field' => 'text NULL',
					'type' => 'textarea',
					'is_show' => 1,
					'placeholder' => '请输入内容'
			],
			'ctime' => [
					'title' => '创建时间',
					'field' => 'int(10) NULL',
					'type' => 'datetime',
					'auto_rule' => 'time',
					'auto_time' => 3,
					'auto_type' => 'function',
					'placeholder' => '请输入内容'
			],
			'vote_id' => [
					'title' => '活动id',
					'field' => 'int(10) NULL',
					'type' => 'num',
					'is_show' => 4,
					'placeholder' => '请输入内容'
			],
			'opt_count' => [
					'title' => '投票数',
					'field' => 'int(10) NULL',
					'type' => 'num',
					'placeholder' => '请输入内容'
			],
			'token' => [
					'title' => 'token',
					'field' => 'varchar(255) NULL',
					'type' => 'string',
					'auto_rule' => 'get_token',
					'auto_time' => 3,
					'auto_type' => 'function',
					'placeholder' => '请输入内容'
			],
			'number' => [
					'title' => '编号',
					'field' => 'int(10) NULL',
					'type' => 'num',
					'value' => 1,
					'placeholder' => '请输入内容'
			],
			'phone' => [
					'title' => '电话',
					'field' => 'varchar(50) NULL',
					'type' => 'string',
					'is_show' => 1,
					'placeholder' => '请输入内容'
			],
			'option_status' => [
					'title' => '审核状态',
					'type' => 'bool',
					'field' => 'tinyint(2) NULL',
					'extra' => '0:未审核
1:已审核',
					'value' => 1,
					'is_show' => 1,
					'is_must' => 0
			]
	];
}	