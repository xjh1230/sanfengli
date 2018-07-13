<?php
/**
 * Vote数据模型
 */
class VoteTable {
	// 数据表模型配置
	public $config = [
			'name' => 'vote',
			'title' => '投票',
			'search_key' => 'title',
			'add_button' => 1,
			'del_button' => 1,
			'search_button' => 1,
			'check_all' => 1,
			'list_row' => 20
	];
	
	// 列表定义
	public $list_grid = [
			'id' => [
					'title' => '投票ID',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [ ]
			],
			'keyword' => [
					'title' => '关键词',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [ ]
			],
			'title' => [
					'title' => '投票标题',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [ ]
			],
			'type' => [
					'title' => '类型',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [ ]
			],
			'is_img' => [
					'title' => '状态',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [ ]
			],
			'vote_count' => [
					'title' => '投票数',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [ ]
			],
			'ids' => [
					'title' => '操作',
					'function' => '',
					'width' => '',
					'sort' => '',
					'href' => [
							'0' => [
									'title' => '编辑',
									'url' => '[EDIT]&id=[id]'
							],
							'1' => [
									'title' => '删除',
									'url' => '[DELETE]'
							],
							'2' => [
									'title' => '投票记录',
									'url' => 'showLog&id=[id]'
							],
							'3' => [
									'title' => '选项票数',
									'url' => 'showCount&id=[id]'
							],
							'4' => [
									'title' => '预览',
									'url' => 'preview?id=[id]&target=_blank'
							]
					]
			]
	];
	
	// 字段定义
	public $fields = [
			'id' => [
					'title' => '主键',
					'field' => 'int(10) unsigned NOT NULL',
					'type' => 'string',
					'is_must' => 1
			],
			'keyword' => [
					'title' => '关键词',
					'field' => 'varchar(50) NOT NULL',
					'type' => 'string',
					'remark' => '用户在微信里回复此关键词将会触发此投票。',
					'is_show' => 1,
					'is_must' => 1,
					'validate_rule' => 'keyword_unique',
					'validate_time' => 1,
					'error_info' => '此关键词已经存在，请换成别的关键词再试试',
					'validate_type' => 'function'
			],
			'title' => [
					'title' => '投票标题',
					'field' => 'varchar(100) NOT NULL',
					'type' => 'string',
					'is_show' => 1,
					'is_must' => 1
			],
			'description' => [
					'title' => '投票描述',
					'field' => 'text NULL',
					'type' => 'textarea',
					'is_show' => 1
			],
			'picurl' => [
					'title' => '封面图片',
					'field' => 'int(10) unsigned NULL',
					'type' => 'picture',
					'remark' => '支持JPG、PNG格式，较好的效果为大图360*200，小图200*200',
					'is_show' => 1
			],
			'type' => [
					'title' => '选择类型',
					'field' => 'char(10) NOT NULL',
					'type' => 'radio',
					'value' => 0,
					'extra' => '0:单选
1:多选',
					'is_must' => 1
			],
			'start_date' => [
					'title' => '开始日期',
					'field' => 'int(10) NULL',
					'type' => 'datetime',
					'is_show' => 1
			],
			'end_date' => [
					'title' => '结束日期',
					'field' => 'int(10) NULL',
					'type' => 'datetime',
					'is_show' => 1
			],
			'is_img' => [
					'title' => '文字/图片投票',
					'field' => 'tinyint(2) NULL',
					'type' => 'radio',
					'value' => 0,
					'extra' => '0:文字投票
1:图片投票',
					'is_must' => 1
			],
			'vote_count' => [
					'title' => '投票数',
					'field' => 'int(10) unsigned NULL',
					'type' => 'num',
					'value' => 0
			],
			'cTime' => [
					'title' => '投票创建时间',
					'field' => 'int(10) NULL',
					'type' => 'datetime',
					'is_must' => 1,
					'auto_rule' => 'time',
					'auto_time' => 3,
					'auto_type' => 'function'
			],
			'mTime' => [
					'title' => '更新时间',
					'field' => 'int(10) NULL',
					'type' => 'datetime',
					'auto_rule' => 'time',
					'auto_time' => 1,
					'auto_type' => 'function'
			],
			'token' => [
					'title' => 'Token',
					'field' => 'varchar(255) NULL',
					'type' => 'string'
			],
			'template' => [
					'title' => '素材模板',
					'field' => 'varchar(255) NULL',
					'type' => 'string',
					'value' => 'default',
					'is_show' => 1
			]
	];
}	