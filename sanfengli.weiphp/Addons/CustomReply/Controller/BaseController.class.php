<?php

namespace Addons\CustomReply\Controller;

use Think\ManageBaseController;

class BaseController extends ManageBaseController {
	var $config;
	function _initialize() {
		parent::_initialize();
		
		$controller = strtolower ( CONTROLLER_NAME );
		
		$res ['title'] = '图文回复';
		$res ['url'] = addons_url ( 'CustomReply://CustomReply/lists' ,$this->get_param);
		$res ['class'] = $controller == 'customreply' ? 'current' : '';
		$nav [] = $res;
		
		$res ['title'] = '多图文设置';
		$res ['url'] = addons_url ( 'CustomReply://CustomReplyMult/lists' ,$this->get_param);
		$res ['class'] = $controller == 'customreplymult' ? 'current' : '';
		$nav [] = $res;
		
		$res ['title'] = '文本回复';
		$res ['url'] = addons_url ( 'CustomReply://CustomReplyText/lists' ,$this->get_param);
		$res ['class'] = $controller == 'customreplytext' ? 'current' : '';
		$nav [] = $res;
		
		$this->assign ( 'nav', $nav );
		
		$config = getAddonConfig ( 'CustomReply' );
		$config ['cover_url'] = get_cover_url ( $config ['cover'] );
		$this->config = $config;
		$this->assign ( 'config', $config );
		// dump ( $config );
		// dump(get_token());
	}
	// 重写的保存关键词方法
	public function _saveKeyword($model, $id, $extra_text) {
		D ( 'Common/Keyword' )->set ( $_POST ['keyword'], MODULE_NAME, $id, $_POST ['keyword_type'], $extra_text );
	}
}
