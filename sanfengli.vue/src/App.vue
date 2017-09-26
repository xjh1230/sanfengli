<template>
  <div id="app">
  
      <!--工具条-->
      <el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
        <el-form :inline="true" :model="conditions" @submit.native.prevent="load">

          <el-form-item>
            <el-input v-model="conditions.name" placeholder="文章关键字"></el-input>
          </el-form-item>
        <el-form-item>
            <el-select v-model="conditions.typeid" placeholder="所属分类" clearable>
              <el-option v-for="item in types" :key="item.name" :label="item.name" :value="item.Id"></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" v-on:click="load" icon="search">查询</el-button>
            <el-button type="primary" v-on:click="showEditForm(emptyruleForm)" icon="plus">新增文章</el-button>
            <el-button type="primary" v-on:click="editTypeVisible=true" icon="plus">新增分类</el-button>
          </el-form-item>
        </el-form>
      </el-col>

        <!-- 列表 -->
      <el-table :data="data" highlight-current-row v-loading="isLoading" id="table">

        <el-table-column type="name" prop="type_name"  label="所属分类"></el-table-column>

        <el-table-column prop="title" label="标题"></el-table-column>
        <el-table-column  label="封面图片">
        <template scope="scope">
            <img v-if="scope.row.image" :src="scope.row.image" class="avatar">
          </template>
        </el-table-column>
        <el-table-column prop="author" label="作者"></el-table-column>
        <el-table-column prop="cTime" label="添加时间" :formatter="AddTimeFormatter"></el-table-column>

        <el-table-column label="操作" min-width="120">
          <template scope="scope">
            <el-button type="text" @click="showEditForm(scope.row)">修改</el-button>
            <el-button type="text" @click="deleteForm(scope.row)">删除</el-button>
           <a target="_blank" v-bind:href="scope.row.url">预览</a>
          </template>
        </el-table-column>

      </el-table>

      <!--分页工具条-->
      <el-col :span="24" class="toolbar">
        共
        <span>{{ total }}</span> 条， 每页显示
        <el-select v-model="size" size="small" style="width: 70px;" @change="load">
          <el-option v-for="item in sizes" :value="item" :label="item" :key="item"></el-option>
        </el-select>
        条
        <el-pagination layout="prev, pager, next" @current-change="handlePageChange" :page-size="size" :total="total" style="float:right;">
        </el-pagination>
      </el-col>

      <el-dialog size='full' :title="ruleForm.Id > 0 ? '修改文章' : '添加文章'" :visible="editFormVisible" :before-close="() => editFormVisible = false">

          <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm" >
                <h2>文章信息管理</h2>
                <el-form-item label="关键字" prop="name" required>
                      <el-input v-model="ruleForm.name"></el-input>
                  </el-form-item>
                  <el-form-item label="标题" prop="title" required>
                      <el-input v-model="ruleForm.title"></el-input>
                  </el-form-item>
                  <el-form-item label="作者" prop="author" required>
                      <el-input v-model="ruleForm.author"></el-input>
                  </el-form-item>
                  <el-form-item>
                    <el-select v-model="ruleForm.type_id" placeholder="所属分类" clearable>
                      <el-option v-for="item in types" :key="item.name" :label="item.name" :value="item.Id"></el-option>
                    </el-select>
                  </el-form-item>
                  <el-form-item label="封面图片" required>
                      <el-upload class="upload-demo" drag :action="fileUploadApi" :on-success="uploadSuccess" :auto-upload="true" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet">
                          <i class="el-icon-upload"></i>
                          <div class="el-upload__text">将文件拖到此处，或
                          <em>点击上传</em>
                          </div>
                          <div style='display:none;' class="el-upload__tip" slot="tip">只能上传png/jpg文件</div>
                      </el-upload>
                       <img width="20%" :src="ruleForm.image" alt="">
                  </el-form-item>
                  
                  <el-form-item label="文章内容" >
                      <el-card>
                              
                              <editor ref="myTextEditor"
                                      :fileName="'myFile'"
                                      :name="'myFile1'"
                                      :canCrop="canCrop"
                                      :uploadUrl="uploadUrl"
                                      @uploadSuccess="onUploadSuccess"
                                      v-model="ruleForm.content"></editor>
                            <div v-html="ruleForm.content"></div>
                      </el-card>
                  </el-form-item>
                
              </el-form>
              <div slot="footer" class="dialog-footer">
                <el-button @click="editFormVisible = false">取消</el-button>
                <el-button type="primary" @click="submitForm('ruleForm')" :loading="editFormLoading">确定</el-button>
              </div>
      </el-dialog>
      <el-dialog  :visible="editTypeVisible"> 
         <el-form :model="typeForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm" >
                <h2>添加分类</h2>
                  <el-form-item label="分类名" prop="name" required>
                      <el-input v-model="typeForm.name"></el-input>
                  </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button @click="editTypeVisible = false">取消</el-button>
            <el-button type="primary" @click="submitFormType('typeForm')" :loading="editFormTypeLoading">确定</el-button>
          </div>
      </el-dialog>

    </div>
</template>

<script>
import editor from './components/Quilleditor.vue'
import server from './components/server'
import moment from 'moment'
import clone from 'clone'
export default {
  name: 'app',
  data(){
    return {
     
      canCrop:true,
      /*测试上传图片的接口，返回结构为{url:''}*/
      uploadUrl:server.base+'/file/uploadImg?path=info',//富文本编辑器上传地址
      fileUploadApi:server.base+'/file/uploadImg?path=info',//封面图片上传地址

      data: [],

      // 搜索
      apiUrl: '/acticle/GetList',
      table: 'Posts',
      order: 'Id',
      desc: true,
      conditions: {
        name: '',
        typeid: '',
      },
      types:[],//文章分类

        // 分页
      total: 0,
      sizes: [10, 20, 50, 100],
      size: 10,
      page: 1,
      // 编辑表单
      editFormVisible: false,
      editFormLoading: false,
      editTypeVisible:false,
      editFormTypeLoading:false,
      ruleForm:{
            id:'',
            author:'',
            name:'',
            title:'',
            cover:'',
            image:'',
            content:'',
            type_id:'',
            dialogVisible:false,
        },
        emptyruleForm:{
            id:'',
            author:'',
            name:'',
            title:'',
            cover:'',
            image:'',
            content:'',
            type_id:'',
        },
        rules: {
            name: [
                { required: true, message: '请输入名称', trigger: 'blur' },
                { min: 2, max: 15, message: '长度在 2 到 15 个字符', trigger: 'blur' }
            ],
            title: [
                {  message: '请输入标题', trigger: 'blur' },
                { min: 0, max: 25, message: '长度在 0 到 25 个字符', trigger: 'blur' }
            ],
            author: [
                {  message: '请输入作者', trigger: 'blur' },
                { min: 0, max: 25, message: '长度在 0 到 25 个字符', trigger: 'blur' }
            ],
        },
        typeForm:{
          name:'',
        }
    }
  },
  methods:{
      load: function () {
          server.post('/acticle/Getypes', {op:'gettypes'}, this).then(res => {
            var tmp=res;
            let { IsSuccess, Data } = res;
            if(IsSuccess){
              this.types = Data;
            }
          });
          let o = {
            page: this.page,
            size: this.size,
            table: this.table,
            conditions: this.conditions,
          };
          server.post(this.apiUrl, {model: o}).then(res => {
          let { IsSuccess, Data,TotalCount } = res;
          console.info(Data);
          console.info(IsSuccess);
            if (IsSuccess) {
              this.total = TotalCount;
              this.data = Data;
            }
          });
      },
    AddTimeFormatter: function (row) { return moment(row.cTime).format('YYYY-MM-DD HH:mm:ss'); },
    showEditForm: function (model) {
      if(model){
        if(model.image){
          model.dialogVisible=true;
        }
        this.ruleForm = clone(model);
      }
      // let selected = [];
      // this.findParents(this.departs, model.ParentId, selected);
      // this.editFormModel.ParentId = selected;

      this.editFormVisible = true;
    },
    deleteForm:function(model){
      this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
           server.post('/acticle/delete', { model: model }, this)
            .then(res => {
              let { IsSuccess, Data,Msg } = res;if (IsSuccess) {
                this.editFormVisible = false;
                this.load();
                this.$message({ type: 'success', message: Msg });
              }  else {
                this.$message({ type: 'error', message: Msg });
              }
            });
        });
      
    },
    handlePageChange: function (page) {
      this.page = page;
      this.load();
    },

    onUploadSuccess(res){//富文本编辑器文件上传成功
      if(res.IsSuccess){
            this.editor.focus();
            this.editor.insertEmbed(this.editor.getSelection().index, 'image', res.Msg);
      }
    },
    uploadSuccess(res){//图片上传成功
        if(res.IsSuccess){
          this.ruleForm.image=res.Msg;
          this.ruleForm.dialogVisible=true;
      }
    },
     submitForm(formName) {
        this.$refs[formName].validate((valid) => {
          let model = clone(this.ruleForm);
          
          this.editFormLoading = true;
          server.post('/acticle/edit', { model: model }, this)
            .then(res => {
            
              let { IsSuccess, Data,Msg } = res;
              let successMsg = model.Id > 0 ? '修改成功(:=' : '添加成功(:=';
              let failureMsg = model.Id > 0 ? '修改失败，请稍后重试(:=' : '添加失败，请稍后重试(:=';
              this.editFormLoading = false;
              if (IsSuccess) {
                this.editFormVisible = false;
                this.load();
                this.$message({ type: 'success', message: Msg });
              }  else {
                this.$message({ type: 'error', message: Msg });
              }
            });
        });
    },
    submitFormType(formName){
        this.editFormTypeLoading = true;
         let model = clone(this.typeForm);
        server.post('/acticle/addType', { model: model }, this)
            .then(res => {
              this.editFormTypeLoading = false;
            
              let { IsSuccess, Data,Msg } = res;
              let successMsg = model.Id > 0 ? '修改成功(:=' : '添加成功(:=';
              let failureMsg = model.Id > 0 ? '修改失败，请稍后重试(:=' : '添加失败，请稍后重试(:=';
              if (IsSuccess) {
                this.editTypeVisible = false;
                this.load();
                this.$message({ type: 'success', message: Msg });
              }  else {
                this.$message({ type: 'error', message: Msg });
              }
            });
    }
  },
  components: {
    editor
  },
   mounted() {
    
    this.load();
  }
}
</script>

<style>
</style>
